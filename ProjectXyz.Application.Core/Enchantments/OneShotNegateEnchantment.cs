using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class OneShotNegateEnchantment : 
        Enchantment, 
        IOneShotNegateEnchantment
    {
        #region Fields
        private readonly Guid _statId;
        #endregion

        #region Constructors
        private OneShotNegateEnchantment(
            IEnchantmentContext context,
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            TimeSpan remainingDuration,
            Guid statId)
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
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid StatId { get { return _statId; } }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantment Create(
            IEnchantmentContext context,
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            TimeSpan remainingDuration,
            Guid statId)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(remainingDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantment>() != null);

            var enchantment = new OneShotNegateEnchantment(
                context,
                id,
                statusTypeId,
                triggerId,
                remainingDuration,
                statId);
            return enchantment;
        }
        #endregion
    }
}
