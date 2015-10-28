using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Plugins.Items.Rare
{
    public sealed class RareItemNamer : IRareItemNamer
    {
        #region Fields
        private readonly IItemNamePartFactory _itemNamePartFactory;
        private readonly IItemNameAffixFilter _itemNameAffixFilter;
        #endregion

        #region Constructors
        private RareItemNamer(
            IItemNamePartFactory itemNamePartFactory,
            IItemNameAffixFilter itemNameAffixFilter)
        {
            _itemNamePartFactory = itemNamePartFactory;
            _itemNameAffixFilter = itemNameAffixFilter;
        }
        #endregion

        #region Methods
        public static IRareItemNamer Create(
            IItemNamePartFactory itemNamePartFactory,
            IItemNameAffixFilter itemNameAffixFilter)
        {
            var itemNamer = new RareItemNamer(
                itemNamePartFactory,
                itemNameAffixFilter);
            return itemNamer;
        }

        public IEnumerable<IItemNamePart> CreateItemName(
            Guid itemTypeId,
            Guid magicTypeId)
        {
            var prefix = _itemNameAffixFilter.GetRandom(
                itemTypeId,
                magicTypeId,
                true);
            var suffix = _itemNameAffixFilter.GetRandom(
                itemTypeId,
                magicTypeId,
                false);

            var partId = Guid.NewGuid();
            yield return _itemNamePartFactory.Create(
                Guid.NewGuid(),
                partId,
                prefix.NameStringResourceId,
                0);
            yield return _itemNamePartFactory.Create(
                Guid.NewGuid(),
                partId,
                suffix.NameStringResourceId,
                1);
        }
        #endregion
    }
}
