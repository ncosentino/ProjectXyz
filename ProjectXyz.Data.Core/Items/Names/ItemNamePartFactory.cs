using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Data.Core.Items.Names
{
    public sealed class ItemNamePartFactory : IItemNamePartFactory
    {
        #region Constructors
        private ItemNamePartFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemNamePartFactory Create()
        {
            var factory = new ItemNamePartFactory();
            return factory;
        }

        public IItemNamePart Create(
            Guid id,
            Guid partId,
            Guid nameStringResourceId,
            int order)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(partId != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(order >= 0);
            Contract.Ensures(Contract.Result<IItemNamePart>() != null);
            
            var itemNamePart = ItemNamePart.Create(
                id,
                partId,
                nameStringResourceId,
                order);
            return itemNamePart;
        }
        #endregion
    }
}
