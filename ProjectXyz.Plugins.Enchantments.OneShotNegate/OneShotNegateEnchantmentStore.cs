using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentStore : 
        EnchantmentStore,
        IOneShotNegateEnchantmentStore
    {
        #region Constructors
        private OneShotNegateEnchantmentStore(
            Guid id,
            Guid enchantmentTypeId,
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId)
            : base(id, enchantmentTypeId, triggerId, statusTypeId, TimeSpan.Zero)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);

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
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentStore Create(
            Guid id,
            Guid enchantmentTypeId,
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return new OneShotNegateEnchantmentStore(
                id,
                enchantmentTypeId,
                statId,
                triggerId,
                statusTypeId);
        }
        #endregion
    }
}
