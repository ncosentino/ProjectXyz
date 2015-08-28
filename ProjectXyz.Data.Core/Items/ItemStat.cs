using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemStat : IItemStat
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemId;
        private readonly Guid _statId;
        #endregion

        #region Constructors
        private ItemStat(
            Guid id,
            Guid itemId,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            
            _id = id;
            _itemId = itemId;
            _statId = statId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid ItemId { get { return _itemId; } }

        /// <inheritdoc />
        public Guid StatId { get { return _statId; } }
        #endregion

        #region Methods
        public static IItemStat Create(
            Guid id,
            Guid itemId,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IItemStat>() != null);
            
            var itemStat = new ItemStat(
                id,
                itemId,
                statId);
            return itemStat;
        }
        #endregion
    }
}
