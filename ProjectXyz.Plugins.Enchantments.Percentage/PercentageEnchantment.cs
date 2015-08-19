using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public sealed class PercentageEnchantment :
        Enchantment,
        IPercentageEnchantment
    {
        #region Constants
        // FIXME: this should be a constant value defined somewhere
        private static readonly Guid ENCHANTMENT_TYPE_ID = Guid.NewGuid();
        #endregion

        #region Fields
        private readonly Guid _statId;
        private readonly double _value;
        #endregion

        #region Constructors
        private PercentageEnchantment(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            IEnumerable<Guid> weatherIds,
            TimeSpan remainingDuration,
            Guid statId,
            double value)
            : base(
                id,
                statusTypeId,
                triggerId,
                ENCHANTMENT_TYPE_ID,
                weatherIds,
                remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(weatherIds != null);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);

            _statId = statId;
            _value = value;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid StatId { get { return _statId; } }

        /// <inheritdoc />
        public double Value { get { return _value; } }
        #endregion

        #region Methods
        public static IPercentageEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            IEnumerable<Guid> weatherIds,
            TimeSpan remainingDuration,
            Guid statId,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(weatherIds != null);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IPercentageEnchantment>() != null);

            var enchantment = new PercentageEnchantment(
                id,
                statusTypeId,
                triggerId,
                weatherIds,
                remainingDuration,
                statId,
                value);
            return enchantment;
        }
        #endregion
    }
}
