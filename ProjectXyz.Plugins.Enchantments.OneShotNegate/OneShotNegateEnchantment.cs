using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantment :
        Enchantment,
        IOneShotNegateEnchantment
    {
        #region Constants
        // FIXME: this should be a constant value defined somewhere
        private static readonly Guid ENCHANTMENT_TYPE_ID = Guid.NewGuid();
        #endregion

        #region Fields
        private readonly Guid _statId;
        #endregion

        #region Constructors
        private OneShotNegateEnchantment(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            Guid weatherTypeGroupingId,
            Guid statId)
            : base(
                id,
                statusTypeId,
                triggerId,
                ENCHANTMENT_TYPE_ID,
                weatherTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(weatherTypeGroupingId != Guid.Empty);
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
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            Guid weatherTypeGroupingId,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(weatherTypeGroupingId != null);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantment>() != null);

            var enchantment = new OneShotNegateEnchantment(
                id,
                statusTypeId,
                triggerId,
                weatherTypeGroupingId,
                statId);
            return enchantment;
        }
        #endregion
    }
}
