using System;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;

namespace ProjectXyz.Plugins.Items.Magic
{
    public sealed class MagicItemGenerator : IItemTypeGenerator
    {
        #region Fields
        private readonly Guid _magicType;
        private readonly IItemFactory _itemFactory;
        private readonly IItemMetaDataFactory _itemMetaDataFactory;
        private readonly IItemTypeGenerator _normalItemGenerator;
        private readonly IMagicAffixGenerator _magicAffixGenerator;
        private readonly IMagicItemNamer _magicItemNamer;
        #endregion

        #region COnstructors
        private MagicItemGenerator(
            Guid magicType,
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemTypeGenerator normalItemGenerator,
            IMagicAffixGenerator magicAffixGenerator,
            IMagicItemNamer magicItemNamer)
        {
            _magicType = magicType;
            _itemFactory = itemFactory;
            _itemMetaDataFactory = itemMetaDataFactory;
            _normalItemGenerator = normalItemGenerator;
            _magicAffixGenerator = magicAffixGenerator;
            _magicItemNamer = magicItemNamer;
        }
        #endregion

        #region Methods
        public static IItemTypeGenerator Create(
            Guid magicType,
            IItemFactory itemFactory,
            IItemMetaDataFactory itemMetaDataFactory,
            IItemTypeGenerator normalItemGenerator,
            IMagicAffixGenerator magicAffixGenerator,
            IMagicItemNamer magicItemNamer)
        {
            var generator = new MagicItemGenerator(
                magicType,
                itemFactory,
                itemMetaDataFactory,
                normalItemGenerator,
                magicAffixGenerator,
                magicItemNamer);
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

            var affixes = _magicAffixGenerator.GenerateAffixes(
                randomizer,
                level,
                _magicType)
                .ToArray();

            var enchantments = affixes
                .SelectMany(x => x.Enchantments)
                .Concat(normalItem.Enchantments);

            var itemNameParts = _magicItemNamer.CreateItemName(
                normalItem.ItemNameParts,
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
        #endregion
    }
}
