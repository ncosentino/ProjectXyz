using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class PercentageEnchantment : 
        Enchantment, 
        IPercentageEnchantment
    {
        #region Fields
        private readonly Guid _statId;
        private readonly double _value;
        #endregion

        #region Constructors
        private PercentageEnchantment(
            IEnchantmentContext context,
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            TimeSpan remainingDuration,
            Guid statId,
            double value)
            : base(
                context,
                id,
                statusTypeId,
                triggerId,
                remainingDuration)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(remainingDuration >= TimeSpan.Zero);
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
            IEnchantmentContext context,
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            TimeSpan remainingDuration,
            Guid statId,
            double value)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(remainingDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IPercentageEnchantment>() != null);

            var enchantment = new PercentageEnchantment(
                context,
                id,
                statusTypeId,
                triggerId,
                remainingDuration,
                statId,
                value);
            return enchantment;
        }
        #endregion
    }
}
