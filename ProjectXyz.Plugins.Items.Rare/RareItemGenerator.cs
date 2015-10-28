using System;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;

namespace ProjectXyz.Plugins.Items.Rare
{
    public sealed class RareItemGenerator : IItemTypeGenerator
    {
        #region Fields
        private readonly Guid _magicType;
        private readonly IItemFactory _itemFactory;
        private readonly IItemMetaDataFactory _itemMetaDataFactory;
        private readonly IItemTypeGenerator _normalItemGenerator;
        private readonly IItemAffixGenerator _itemAffixGenerator;
        private readonly IRareItemNamer _itemNamer;
        #endregion

        #region COnstructors
        private RareItemGenerator(
            Guid magicType,
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemTypeGenerator normalItemGenerator,
            IItemAffixGenerator itemAffixGenerator,
            IRareItemNamer itemNamer)
        {
            _magicType = magicType;
            _itemFactory = itemFactory;
            _itemMetaDataFactory = itemMetaDataFactory;
            _normalItemGenerator = normalItemGenerator;
            _itemAffixGenerator = itemAffixGenerator;
            _itemNamer = itemNamer;
        }
        #endregion

        #region Methods
        public static IItemTypeGenerator Create(
            Guid magicType,
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemTypeGenerator normalItemGenerator,
            IItemAffixGenerator itemAffixGenerator,
            IRareItemNamer itemNamer)
        {
            var generator = new RareItemGenerator(
                magicType,
                itemFactory,
                itemMetaDataFactory,
                normalItemGenerator,
                itemAffixGenerator,
                itemNamer);
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

            var affixes = _itemAffixGenerator.GenerateRandom(
                randomizer,
                level,
                _magicType)
                .ToArray();

            var enchantments = affixes
                .SelectMany(x => x.Enchantments)
                .Concat(normalItem.Enchantments);

            var itemNameParts = _itemNamer.CreateItemName(
                normalItem.ItemTypeId,
                _magicType);

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
        #endregion
    }
}
