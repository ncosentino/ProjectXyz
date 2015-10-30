using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Plugins.Items.Normal
{
    public sealed class NormalItemGenerator : IItemTypeGenerator
    {
        #region Fields
        private readonly IItemFactory _itemFactory;
        private readonly IItemMetaDataFactory _itemMetaDataFactory;
        private readonly IItemDataManager _itemDataManager;
        private readonly IItemNamePartFactory _itemNamePartFactory;
        private readonly IStatGenerator _statGenerator;
        private readonly IItemRequirementsGenerator _itemRequirementsgenerator;

        #endregion

        #region Constructors
        private NormalItemGenerator(
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemNamePartFactory itemNamePartFactory,
            IItemDataManager itemDataManager,
            IStatGenerator statGenerator,
            IItemRequirementsGenerator itemRequirementsGenerator)
        {
            _itemFactory = itemFactory;
            _itemMetaDataFactory = itemMetaDataFactory;
            _itemNamePartFactory = itemNamePartFactory;
            _itemDataManager = itemDataManager;
            _statGenerator = statGenerator;
            _itemRequirementsgenerator = itemRequirementsGenerator;
        }
        #endregion

        #region Methods
        public static IItemTypeGenerator Create(
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemRequirementsFactory itemRequirementsFactory,
            IItemNamePartFactory itemNamePartFactory,
            IStatRepository statRepository,
            IItemDataManager itemDataManager,
            IStatGenerator statGenerator,
            IItemRequirementsGenerator itemRequirementsGenerator)
        {
            var generator = new NormalItemGenerator(
                itemFactory,
                itemMetaDataFactory,
                itemNamePartFactory,
                itemDataManager,
                statGenerator,
                itemRequirementsGenerator);
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
                itemDefinition.InventoryGraphicResourceId,
                itemDefinition.MagicTypeId, // NOTE: this allows items like potions to be spawned as "magic" but have no affixes
                itemDefinition.ItemTypeId,
                itemDefinition.MaterialTypeId,
                itemDefinition.SocketTypeId);

            var itemStats = _statGenerator.GenerateStats(
                randomizer,
                itemDefinitionStats.Cast<IStatRange>());

            var equippableSlots = _itemDataManager.ItemTypeEquipSlotType
                .GetByItemTypeId(itemDefinition.ItemTypeId)
                .Select(x => x.EquipSlotTypeId);

            var enchantments = GenerateEnchantments();
            var itemRequirements = _itemRequirementsgenerator.GenerateItemRequirements(itemDefinitionId);

            var itemNamePart = _itemNamePartFactory.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemDefinition.NameStringResourceId,
                0);

            var item = _itemFactory.Create(
                itemContext,
                Guid.NewGuid(),
                itemDefinitionId,
                itemMetaData,
                new[] { itemNamePart },
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
        #endregion
    }
}
