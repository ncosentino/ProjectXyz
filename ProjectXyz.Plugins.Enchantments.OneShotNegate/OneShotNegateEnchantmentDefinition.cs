using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentDefinition : IOneShotNegateEnchantmentDefinition
    {
        #region Constructors
        private OneShotNegateEnchantmentDefinition(
            Guid id,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);

            this.Id = id;
            this.StatId = statId;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            set;
        }

        public Guid StatId
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentDefinition Create(
            Guid id,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentDefinition>() != null);

            var enchantmentDefinition = new OneShotNegateEnchantmentDefinition(
                id,
                statId);
            return enchantmentDefinition;
        }
        #endregion
    }
}
