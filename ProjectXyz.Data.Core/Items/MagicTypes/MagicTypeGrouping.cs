using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.MagicTypes;

namespace ProjectXyz.Data.Core.Items.MagicTypes
{
    public sealed class MagicTypeGrouping : IMagicTypeGrouping
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _groupingId;
        private readonly Guid _magicTypeId;
        #endregion

        #region Constructors
        private MagicTypeGrouping(
            Guid id,
            Guid groupingId,
            Guid magicTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(groupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            
            _id = id;
            _groupingId = groupingId;
            _magicTypeId = magicTypeId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid GroupingId { get { return _groupingId; } }

        /// <inheritdoc />
        public Guid MagicTypeId { get { return _magicTypeId; } }
        #endregion

        #region Methods
        public static IMagicTypeGrouping Create(
            Guid id,
            Guid groupingId,
            Guid magicTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(groupingId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IMagicTypeGrouping>() != null);
            
            var magicTypeGrouping = new MagicTypeGrouping(
                id,
                groupingId,
                magicTypeId);
            return magicTypeGrouping;
        }
        #endregion
    }
}
