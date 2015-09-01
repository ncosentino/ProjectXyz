using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Core.Items
{
    public sealed class ItemDefinitionStat : IItemDefinitionStat
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _itemDefinitionId;
        private readonly Guid _statDefinitionId;
        private readonly double _minimumValue;
        private readonly double _maximumValue;
        #endregion

        #region Constructors
        private ItemDefinitionStat(
            Guid id,
            Guid itemDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);

            _id = id;
            _itemDefinitionId = itemDefinitionId;
            _statDefinitionId = statDefinitionId;
            _minimumValue = minimumValue;
            _maximumValue = maximumValue;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid ItemDefinitionId { get { return _itemDefinitionId; } }

        /// <inheritdoc />
        public Guid StatDefinitionId { get { return _statDefinitionId; } }

        /// <inheritdoc />
        public double MinimumValue { get { return _minimumValue; } }

        /// <inheritdoc />
        public double MaximumValue { get { return _maximumValue; } }
        #endregion

        #region Methods
        public static IItemDefinitionStat Create(
            Guid id,
            Guid itemDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(itemDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);
            Contract.Ensures(Contract.Result<IItemDefinitionStat>() != null);
            
            var itemDefinitionStat = new ItemDefinitionStat(
                id,
                itemDefinitionId,
                statDefinitionId,
                minimumValue,
                maximumValue);
            return itemDefinitionStat;
        }
        #endregion
    }
}
