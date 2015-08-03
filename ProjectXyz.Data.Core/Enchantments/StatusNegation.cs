using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class StatusNegation : IStatusNegation
    {
        #region Fields
        private readonly Guid _statId;
        private readonly Guid _enchantmentStatusId;
        #endregion

        #region Constructors
        private StatusNegation(
            Guid statId,
            Guid enchantmentStatusId)
        {
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentStatusId != Guid.Empty);

            _statId = statId;
            _enchantmentStatusId = enchantmentStatusId;
        }
        #endregion

        #region Properties
        public Guid StatId 
        {
            get { return _statId; }
        }

        public Guid EnchantmentStatusId
        {
            get { return _enchantmentStatusId; }
        }
        #endregion

        #region Methods
        public static IStatusNegation Create(
            Guid statId,
            Guid enchantmentStatusId)
        {
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentStatusId != Guid.Empty);
            Contract.Ensures(Contract.Result<IStatusNegation>() != null);

            return new StatusNegation(statId, enchantmentStatusId);
        }
        #endregion
    }
}
