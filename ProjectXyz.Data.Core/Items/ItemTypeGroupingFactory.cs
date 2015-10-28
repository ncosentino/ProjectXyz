using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemTypeGroupingFactory : IItemTypeGroupingFactory
    {
        #region Constructors
        private ItemTypeGroupingFactory()
        {
        }
        #endregion

        #region Methods
        public static IItemTypeGroupingFactory Create()
        {
            var factory = new ItemTypeGroupingFactory();
            return factory;
        }

        public IItemTypeGrouping Create(
            Guid id,
            Guid groupingId,
            Guid itemTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(groupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemTypeGrouping>() != null);
            
            var itemTypeGrouping = ItemTypeGrouping.Create(
                id,
                groupingId,
                itemTypeId);
            return itemTypeGrouping;
        }
        #endregion
    }
}
