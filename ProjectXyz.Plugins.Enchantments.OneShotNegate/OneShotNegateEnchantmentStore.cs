using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentStore : IOneShotNegateEnchantmentStore
    {
        #region Constructors
        private OneShotNegateEnchantmentStore(
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
        /// <inheritdoc />
        public Guid StatId
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Guid Id
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentStore Create(
            Guid id,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentStore>() != null);

            return new OneShotNegateEnchantmentStore(
                id,
                statId);
        }
        #endregion
    }
}
