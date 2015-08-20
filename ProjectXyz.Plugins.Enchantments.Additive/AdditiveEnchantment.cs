using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public sealed class AdditiveEnchantment :
        Enchantment,
        IAdditiveEnchantment
    {
        #region Constants
        // FIXME: this should be a constant value defined somewhere
        private static readonly Guid ENCHANTMENT_TYPE_ID = Guid.NewGuid();
        #endregion

        #region Fields
        private readonly Guid _statId;
        private readonly double _value;
        private readonly TimeSpan _remainingDuration;
        #endregion

        #region Constructors
        private AdditiveEnchantment(
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
                weatherIds)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(weatherIds != null);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);

            _statId = statId;
            _value = value;
            _remainingDuration = remainingDuration;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid StatId { get { return _statId; } }

        /// <inheritdoc />
        public double Value { get { return _value; } }

        /// <inheritdoc />
        public TimeSpan RemainingDuration { get { return _remainingDuration; } }
        #endregion

        #region Methods
        public static IAdditiveEnchantment Create(
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
            Contract.Ensures(Contract.Result<IAdditiveEnchantment>() != null);

            var enchantment = new AdditiveEnchantment(
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
