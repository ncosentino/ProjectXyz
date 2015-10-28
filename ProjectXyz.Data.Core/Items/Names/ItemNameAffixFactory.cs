using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Data.Core.Items.Names
{
    public sealed class ItemNameAffixFactory : IItemNameAffixFactory
    {
        #region Constructors
        private ItemNameAffixFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemNameAffixFactory Create()
        {
            var factory = new ItemNameAffixFactory();
            return factory;
        }

        public IItemNameAffix Create(
            Guid id,
            bool isPrefix,
            Guid itemTypeGroupingId,
            Guid magicTypeGroupingId,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeGroupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeGroupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemNameAffix>() != null);
            
            var itemNameAffix = ItemNameAffix.Create(
                id,
                isPrefix,
                itemTypeGroupingId,
                magicTypeGroupingId,
                nameStringResourceId);
            return itemNameAffix;
        }
        #endregion
    }
}
