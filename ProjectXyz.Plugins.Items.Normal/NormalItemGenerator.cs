using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Plugins.Items.Normal
{
    public sealed class NormalItemGenerator : IItemTypeGenerator
    {
        #region Fields
        private readonly IItemFactory _itemFactory;
        private readonly IItemMetaDataFactory _itemMetaDataFactory;
        private readonly IStatFactory _statFactory;
        private readonly IItemRequirementsFactory _itemRequirementsFactory;
        private readonly IStatRepository _statRepository;
        private readonly IItemDataManager _itemDataManager;
        #endregion

        #region COnstructors
        private NormalItemGenerator(
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemRequirementsFactory itemRequirementsFactory,
            IStatFactory statFactory,
            IStatRepository statRepository,
            IItemDataManager itemDataManager)
        {
            _itemFactory = itemFactory;
            _itemMetaDataFactory = itemMetaDataFactory;
            _statFactory = statFactory;
            _itemRequirementsFactory = itemRequirementsFactory;
            _statRepository = statRepository;
            _itemDataManager = itemDataManager;
        }
        #endregion

        #region Methods
        public static IItemTypeGenerator Create(
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemRequirementsFactory itemRequirementsFactory,
            IStatFactory statFactory,
            IStatRepository statRepository,
            IItemDataManager itemDataManager)
        {
            var generator = new NormalItemGenerator(
                itemFactory,
                itemMetaDataFactory,
                itemRequirementsFactory,
                statFactory,
                statRepository,
                itemDataManager);
            return generator;
        }

        public IItem Generate(
           IRandom randomizer,
            Guid itemDefinitionId,
            int level,
            IItemContext itemContext)
        {
            var itemDefinition = _itemDataManager.ItemDefinitions.GetById(itemDefinitionId);
            var itemDefinitionStats = _itemDataManager.ItemDefinitionStat.GetByItemDefinitionId(itemDefinitionId);

            var itemMetaData = _itemMetaDataFactory.Create(
                itemDefinition.NameStringResourceId,
                itemDefinition.InventoryGraphicResourceId,
                itemDefinition.MagicTypeId, // NOTE: this allows items like potions to be spawned as "magic" but have no affixes
                itemDefinition.ItemTypeId,
                itemDefinition.MaterialTypeId,
                itemDefinition.SocketTypeId);

            var itemStats = GenerateItemStats(
                randomizer,
                _statFactory,
                itemDefinitionStats);

            var equippableSlots = _itemDataManager.ItemTypeEquipSlotType
                .GetByItemTypeId(itemDefinition.ItemTypeId)
                .Select(x => x.EquipSlotTypeId);

            var enchantments = GenerateEnchantments();

            var requiredStats = _itemDataManager.ItemDefinitionStatRequirements
                .GetByItemDefinitionId(itemDefinitionId)
                .Select(x => _statRepository.GetById(x.StatId));
            var itemDefinitionItemMiscRequirements = _itemDataManager.ItemDefinitionItemMiscRequirements.GetByItemDefinitionId(itemDefinitionId);
            var itemMiscRequirements = _itemDataManager.ItemMiscRequirements.GetById(itemDefinitionItemMiscRequirements.ItemMiscRequirementsId);
            var itemRequirements = _itemRequirementsFactory.Create(
                itemMiscRequirements.RaceDefinitionId,
                itemMiscRequirements.ClassDefinitionId,
                requiredStats);

            var item = _itemFactory.Create(
                itemContext,
                Guid.NewGuid(),
                itemDefinitionId,
                itemMetaData,
                itemRequirements,
                itemStats,
                enchantments,
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                equippableSlots);
            return item;
        }

        private IEnumerable<IEnchantment> GenerateEnchantments()
        {
            // TODO: allow base items to have enchantments... things like potions especially.
            return Enumerable.Empty<IEnchantment>();
        }

        private IEnumerable<IStat> GenerateItemStats(
            IRandom randomizer,
            IStatFactory statFactory,
            IEnumerable<IItemDefinitionStat> itemDefinitionStats)
        {
            return itemDefinitionStats
                .Select(x =>
                {
                    var value =
                        x.MinimumValue +
                        randomizer.NextDouble() * (x.MaximumValue - x.MinimumValue);
                    var stat = statFactory.Create(
                        Guid.NewGuid(),
                        x.StatDefinitionId,
                        value);
                    return stat;
                });
        }
        #endregion
    }
}
