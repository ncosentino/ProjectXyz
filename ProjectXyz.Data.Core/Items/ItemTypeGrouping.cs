using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemTypeGrouping : IItemTypeGrouping
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _groupingId;
        private readonly Guid _itemTypeId;
        #endregion

        #region Constructors
        private ItemTypeGrouping(
            Guid id,
            Guid groupingId,
            Guid itemTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(groupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeId != Guid.Empty);
            
            _id = id;
            _groupingId = groupingId;
            _itemTypeId = itemTypeId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid GroupingId { get { return _groupingId; } }

        /// <inheritdoc />
        public Guid ItemTypeId { get { return _itemTypeId; } }
        #endregion

        #region Methods
        public static IItemTypeGrouping Create(
            Guid id,
            Guid groupingId,
            Guid itemTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(groupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(itemTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemTypeGrouping>() != null);
            
            var itemTypeGrouping = new ItemTypeGrouping(
                id,
                groupingId,
                itemTypeId);
            return itemTypeGrouping;
        }
        #endregion
    }
}
