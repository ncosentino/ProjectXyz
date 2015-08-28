using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemTypeFactory : IItemTypeFactory
    {
        #region Constructors
        private ItemTypeFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemTypeFactory Create()
        {
            var factory = new ItemTypeFactory();
            return factory;
        }

        /// <inheritdoc />
        public IItemType Create(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemType>() != null);
            
            var itemType = ItemType.Create(
                id,
                nameStringResourceId);
            return itemType;
        }
        #endregion
    }
}
