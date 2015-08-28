using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Core.Items.Requirements
{
    public sealed class ItemStatRequirements : IItemStatRequirements
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemId;
        private readonly Guid _statId;
        #endregion

        #region Constructors
        private ItemStatRequirements(
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
        public static IItemStatRequirements Create(
            Guid id,
            Guid itemId,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);

            Contract.Ensures(Contract.Result<IItemStatRequirements>() != null);

            var itemStatRequirements = new ItemStatRequirements(
                id,
                itemId,
                statId);
            return itemStatRequirements;
        }
        #endregion
    }
}