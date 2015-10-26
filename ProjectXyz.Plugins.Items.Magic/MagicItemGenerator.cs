using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Plugins.Items.Magic
{
    public sealed class MagicItemGenerator : IItemTypeGenerator
    {
        #region Fields
        private readonly Guid _magicType;
        private readonly IItemFactory _itemFactory;
        private readonly IItemMetaDataFactory _itemMetaDataFactory;
        private readonly IItemTypeGenerator _normalItemGenerator;
        private readonly IItemAffixGenerator _itemAffixGenerator;
        private readonly IItemDataManager _itemDataManager;
        private readonly IItemNamePartFactory _itemNamePartFactory;
        #endregion

        #region COnstructors
        private MagicItemGenerator(
            Guid magicType,
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemNamePartFactory itemNamePartFactory,
            IItemTypeGenerator normalItemGenerator,
            IItemAffixGenerator itemAffixGenerator,
            IItemDataManager itemDataManager)
        {
            _magicType = magicType;
            _itemFactory = itemFactory;
            _itemMetaDataFactory = itemMetaDataFactory;
            _itemNamePartFactory = itemNamePartFactory;
            _normalItemGenerator = normalItemGenerator;
            _itemAffixGenerator = itemAffixGenerator;
            _itemDataManager = itemDataManager;
        }
        #endregion

        #region Methods
        public static IItemTypeGenerator Create(
            Guid magicType,
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemNamePartFactory itemNamePartFactory,
            IItemTypeGenerator normalItemGenerator,
            IItemAffixGenerator itemAffixGenerator,
            IItemDataManager itemDataManager)
        {
            var generator = new MagicItemGenerator(
                magicType,
                itemFactory,
                itemMetaDataFactory,
                itemNamePartFactory,
                normalItemGenerator,
                itemAffixGenerator,
                itemDataManager);
            return generator;
        }

        public IItem Generate(
            IRandom randomizer,
            Guid itemDefinitionId,
            int level,
            IItemContext itemContext)
        {
            var normalItem = _normalItemGenerator.Generate(
                randomizer,
                itemDefinitionId,
                level,
                itemContext);

            var itemMetaData = _itemMetaDataFactory.Create(
                normalItem.InventoryGraphicResourceId,
                _magicType,
                normalItem.ItemTypeId,
                normalItem.MaterialTypeId,
                normalItem.SocketTypeId);

            var affixes = GenerateAffixes(
                _itemAffixGenerator,
                randomizer,
                level,
                _magicType)
                .ToArray();

            var enchantments = affixes
                .SelectMany(x => x.Enchantments)
                .Concat(normalItem.Enchantments);

            var itemNameParts = CreateItemName(
                normalItem,
                affixes);

            var item = _itemFactory.Create(
                itemContext,
                Guid.NewGuid(),
                itemDefinitionId,
                itemMetaData,
                itemNameParts,
                normalItem.Requirements,
                normalItem.Stats,
                enchantments,
                affixes,
                Enumerable.Empty<IItem>(),
                normalItem.EquippableSlotIds);
            return item;
        }

        private IEnumerable<IItemNamePart> CreateItemName(
            IItem normalItem,
            IEnumerable<IItemAffix> affixes)
        {
            // give our item a nice new name based on the affixes
            var nameParts = new List<IItemNamePart>(normalItem.ItemNameParts);
            var originalNamePart = normalItem.ItemNameParts.First();

            var prefix = affixes.FirstOrDefault(x => x.Prefix);
            if (prefix != null)
            {
                nameParts.Add(_itemNamePartFactory.Create(
                    Guid.NewGuid(),
                    originalNamePart.PartId,
                    prefix.NameStringResourceId,
                    0));

                // re-insert the original name part at index 1
                nameParts.Remove(originalNamePart);
                originalNamePart = _itemNamePartFactory.Create(
                    originalNamePart.Id,
                    originalNamePart.PartId,
                    originalNamePart.NameStringResourceId,
                    1);
                nameParts.Add(originalNamePart);
            }

            var suffix = affixes.FirstOrDefault(x => !x.Prefix);
            if (suffix != null)
            {
                nameParts.Add(_itemNamePartFactory.Create(
                    Guid.NewGuid(),
                    originalNamePart.PartId,
                    suffix.NameStringResourceId,
                    originalNamePart.Order + 1));
            }

            return nameParts;
        }

        private IEnumerable<IItemAffix> GenerateAffixes(
            IItemAffixGenerator itemAffixGenerator,
            IRandom randomizer,
            int level, 
            Guid magicTypeId)
        {
            var magicTypesRandomAffixes = _itemDataManager.MagicTypesRandomAffixes.GetForMagicTypeId(magicTypeId);

            var targetAffixCount = 
                (int)Math.Round(magicTypesRandomAffixes.MinimumAffixes + 
                randomizer.NextDouble() * (magicTypesRandomAffixes.MaximumAffixes - magicTypesRandomAffixes.MinimumAffixes));

            bool gotPrefix = false;
            var generatesAffixDefinitionIds = new HashSet<Guid>();

            while (generatesAffixDefinitionIds.Count < targetAffixCount)
            {
                var shouldGeneratePrefix = (magicTypesRandomAffixes.MaximumAffixes != 2 || !gotPrefix) && randomizer.NextDouble() >= 0.5d;

                IItemAffix affix;
                if (shouldGeneratePrefix)
                {
                    affix = itemAffixGenerator.GeneratePrefix(
                        randomizer,
                        level,
                        magicTypeId);
                    gotPrefix = true;
                }
                else
                {
                    affix = itemAffixGenerator.GenerateSuffix(
                        randomizer,
                        level,
                        magicTypeId);
                }

                if (generatesAffixDefinitionIds.Contains(affix.ItemAffixDefinitionId))
                {
                    continue;
                }

                generatesAffixDefinitionIds.Add(affix.ItemAffixDefinitionId);
                yield return affix;
            }
        }
        #endregion
    }
}
