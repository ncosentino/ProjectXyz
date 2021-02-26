using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.ItemSets
{
    public sealed class ItemSetManager : IItemSetManager
    {
        private readonly IItemSetDefinitionRepositoryFacade _itemSetDefinitionRepositoryFacade;
        private readonly IEnchantmentLoader _enchantmentLoader;
        private readonly IEnchantmentFactory _enchantmentFactory;
        private readonly IFilterContextFactory _filterContextFactory;

        public ItemSetManager(
            IItemSetDefinitionRepositoryFacade itemSetDefinitionRepositoryFacade,
            IEnchantmentLoader enchantmentLoader,
            IEnchantmentFactory enchantmentFactory,
            IFilterContextFactory filterContextFactory)
        {
            _itemSetDefinitionRepositoryFacade = itemSetDefinitionRepositoryFacade;
            _enchantmentLoader = enchantmentLoader;
            _enchantmentFactory = enchantmentFactory;
            _filterContextFactory = filterContextFactory;
        }

        public void UpdateItemSets(IGameObject actor)
        {
            var carriedItems = actor
                .Get<IHasItemContainersBehavior>()
                .SelectMany(x => x.ItemContainers.SelectMany(c => c.Items))
                .SelectMany(x => x.Get<IBelongsToItemSetBehavior>())
                .Select(x => new
                {
                    Item = (IGameObject)x.Owner, // FIXME: wtf is this casting...
                    BelongsToSet = x,
                })
                .ToArray();
            var equippedItems = actor
                .GetOnly<IHasEquipmentBehavior>()
                .GetEquippedItemsBySlot()
                .Select(x => new
                {
                    EquipSlotId = x.Item1,
                    ItemSetBehaviors = x.Item2.Get<IBelongsToItemSetBehavior>()
                })
                .SelectMany(x => x.ItemSetBehaviors.Select(isb => new
                {
                    EquipSlotId = x.EquipSlotId,
                    Item = (IGameObject)isb.Owner, // FIXME: wtf is this casting...
                    BelongsToSet = isb,
                }))
                .ToArray();
            var itemSetDefinitionIds = carriedItems
                .Select(x => x.BelongsToSet.ItemSetId)
                .Concat(equippedItems
                .Select(x => x.BelongsToSet.ItemSetId))
                .Distinct()
                .ToArray();

            var itemSetEnchantmentsToAdd = new List<IEnchantment>();
            foreach (var itemSetDefinitionId in itemSetDefinitionIds)
            {
                var itemSetDefinition = _itemSetDefinitionRepositoryFacade.GetItemSetDefinitionById(itemSetDefinitionId);

                var itemSetUniqueIdsToSkip = new HashSet<IIdentifier>();
                var equippedItemsForSet = new HashSet<IGameObject>();
                foreach (var itemEntry in equippedItems)
                {
                    if (!itemEntry.BelongsToSet.ItemSetId.Equals(itemSetDefinition.Id))
                    {
                        continue;
                    }

                    // skip over this if we're not allowed to double count it towards set bonuses
                    if (itemEntry.BelongsToSet.UniqueIdWithinSet != null)
                    {
                        if (itemSetUniqueIdsToSkip.Contains(itemEntry.BelongsToSet.UniqueIdWithinSet))
                        {
                            continue;
                        }

                        itemSetUniqueIdsToSkip.Add(itemEntry.BelongsToSet.UniqueIdWithinSet);
                    }

                    equippedItemsForSet.Add(itemEntry.Item);
                }

                var carriedItemsForSet = new HashSet<IGameObject>();
                foreach (var itemEntry in carriedItems)
                {
                    if (itemEntry.BelongsToSet.MustBeEquipped ||
                        !itemEntry.BelongsToSet.ItemSetId.Equals(itemSetDefinition.Id))
                    {
                        continue;
                    }

                    // skip over this if we're not allowed to double count it towards set bonuses
                    if (itemEntry.BelongsToSet.UniqueIdWithinSet != null)
                    {
                        if (itemSetUniqueIdsToSkip.Contains(itemEntry.BelongsToSet.UniqueIdWithinSet))
                        {
                            continue;
                        }

                        itemSetUniqueIdsToSkip.Add(itemEntry.BelongsToSet.UniqueIdWithinSet);
                    }

                    carriedItemsForSet.Add(itemEntry.Item);
                }

                // use this count for matching stuff now
                var totalMatchingItems = carriedItemsForSet.Count + equippedItemsForSet.Count;

                // add any enchantments that are specific to having equip slots filled
                var enchantmentsForEquipSlots = itemSetDefinition
                    .EnchantmentsForEquipSlots
                    .ToDictionary(x => x.EquipSlotId, x => x);
                foreach (var item in equippedItemsForSet)
                {
                    var equipSlotId = equippedItems
                        .First(x => x.Item == item)
                        .EquipSlotId;
                    if (enchantmentsForEquipSlots.TryGetValue(
                        equipSlotId,
                        out var itemSetMatchingEquipSlotEnchantments) &&
                        totalMatchingItems >= itemSetMatchingEquipSlotEnchantments.MinimumRequiredItemsInSet)
                    {
                        itemSetEnchantmentsToAdd.AddRange(LoadEnchantmentsByDefinitionIds(
                            itemSetDefinitionId,
                            itemSetMatchingEquipSlotEnchantments.EnchantmentDefinitionIds));
                    }
                }

                // add the enchantments for matching items
                foreach (var item in equippedItemsForSet.Concat(carriedItemsForSet))
                {
                    foreach (var itemSetMatchingItemEnchantments in itemSetDefinition.EnchantmentsForMatchingItems)
                    {
                        if (totalMatchingItems < itemSetMatchingItemEnchantments.MinimumRequiredItemsInSet)
                        {
                            continue;
                        }

                        var itemTemplateId = item
                            .GetOnly<ITemplateIdentifierBehavior>()
                            .TemplateId;
                        if (!itemTemplateId.Equals(itemSetMatchingItemEnchantments.ItemTemplateId))
                        {
                            continue;
                        }

                        itemSetEnchantmentsToAdd.AddRange(LoadEnchantmentsByDefinitionIds(
                            itemSetDefinitionId,
                            itemSetMatchingItemEnchantments.EnchantmentDefinitionIds));
                    }
                }

                // add enchantments based on the number of matching pieces
                foreach (var enchantmentsForCountOfSet in itemSetDefinition
                    .EnchantmentsForCountsOfSet
                    .OrderBy(x => x.Key))
                {
                    var countRequired = enchantmentsForCountOfSet.Key;
                    if (countRequired > totalMatchingItems)
                    {
                        break;
                    }

                    if (itemSetDefinition.CountEnchantmentsAreAdditive)
                    {
                        if (totalMatchingItems >= countRequired)
                        {
                            itemSetEnchantmentsToAdd.AddRange(LoadEnchantmentsByDefinitionIds(
                                itemSetDefinitionId,
                                enchantmentsForCountOfSet.Value));
                        }

                        continue;
                    }

                    if (totalMatchingItems == countRequired)
                    {
                        itemSetEnchantmentsToAdd.AddRange(LoadEnchantmentsByDefinitionIds(
                            itemSetDefinitionId,
                            enchantmentsForCountOfSet.Value));
                        break;
                    }
                }
            }

            ApplyItemSetEnchantments(actor, itemSetEnchantmentsToAdd);
        }

        private static void ApplyItemSetEnchantments(
            IGameObject actor,
            IReadOnlyCollection<IEnchantment> itemSetEnchantmentsToAdd)
        {
            var actorEnchantmentsBehavior = actor.GetOnly<IHasEnchantmentsBehavior>();
            var currentEnchantments = actorEnchantmentsBehavior
                .Enchantments
                .SelectMany(x => x
                    .Behaviors
                    .TakeTypes<IItemSetEnchantmentBehavior>()
                    .Select(iseb => new
                    {
                        EnchantmentDefinitionId = iseb.EnchantmentDefinitionId,
                        ItemSetId = iseb.ItemSetId,
                        Enchantment = (IEnchantment)iseb.Owner, // FIXME: wtf is this casting
                    }))
                .GroupBy(x => x.EnchantmentDefinitionId, x => x.Enchantment)
                .ToDictionary(x => x.Key, x => x.ToReadOnlyCollection());
            var enchantmentsToAdd = itemSetEnchantmentsToAdd
                .SelectMany(x => x
                    .Behaviors
                    .TakeTypes<IItemSetEnchantmentBehavior>()
                    .Select(iseb => new
                    {
                        EnchantmentDefinitionId = iseb.EnchantmentDefinitionId,
                        ItemSetId = iseb.ItemSetId,
                        Enchantment = (IEnchantment)iseb.Owner, // FIXME: wtf is this casting
                    }))
                .GroupBy(x => x.EnchantmentDefinitionId, x => x.Enchantment)
                .ToDictionary(x => x.Key, x => x.ToReadOnlyCollection());

            foreach (var entryToAdd in enchantmentsToAdd)
            {
                if (currentEnchantments.TryGetValue(
                    entryToAdd.Key,
                    out var current))
                {
                    if (current.Count < entryToAdd.Value.Count)
                    {
                        actorEnchantmentsBehavior
                            .AddEnchantments(entryToAdd
                                .Value
                                .Skip(entryToAdd.Value.Count - current.Count));
                    }
                    else if (current.Count > entryToAdd.Value.Count)
                    {
                        actorEnchantmentsBehavior
                            .RemoveEnchantments(current
                            .ToArray() // we need a copy here
                            .Skip(current.Count - entryToAdd.Value.Count));
                    }

                    // mark this as handled by ignoring it going forward
                    currentEnchantments.Remove(entryToAdd.Key);
                }
                else
                {
                    actorEnchantmentsBehavior.AddEnchantments(entryToAdd.Value);
                }
            }

            // remove what's leftover
            actorEnchantmentsBehavior.RemoveEnchantments(currentEnchantments.SelectMany(x => x.Value));
        }

        // FIXME: can this be an extension method somewhere? Where would it 
        // live? can we even assume all enchantment definitions will have a
        // filter attribute ID of 'id'? (not the enchantment ID part)
        private IEnchantment LoadEnchantmentByDefinitionId(
            IIdentifier itemSetId,
            IIdentifier enchantmentDefinitionId)
        {
            // FIXME: optimize this to take multiple enchantment definition 
            // IDs and use a filter attribute value that is a collection of
            // identifiers (i.e. All/Any string collection example)
            var filterContext = _filterContextFactory.CreateFilterContextForSingle(new FilterAttribute(
                new StringIdentifier("id"),
                new IdentifierFilterAttributeValue(enchantmentDefinitionId),
                true));
            var enchantment = _enchantmentLoader
                .Load(filterContext)
                .Single();
            enchantment = _enchantmentFactory.Create(enchantment
                .Behaviors
                .AppendSingle(new ItemSetEnchantmentBehavior(
                    itemSetId,
                    enchantmentDefinitionId)));
            return enchantment;
        }

        private IEnumerable<IEnchantment> LoadEnchantmentsByDefinitionIds(
            IIdentifier itemSetId, 
            IEnumerable<IIdentifier> enchantmentDefinitionIds)
        {
            return enchantmentDefinitionIds
                .Select(enchantmentDefinitionId => LoadEnchantmentByDefinitionId(
                    itemSetId,
                    enchantmentDefinitionId));
        }
    }
}
