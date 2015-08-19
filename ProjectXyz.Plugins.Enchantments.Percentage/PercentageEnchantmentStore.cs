using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public sealed class PercentageEnchantmentStore : IPercentageEnchantmentStore
    {
        #region Constructors
        private PercentageEnchantmentStore(
            Guid id,
            Guid statId,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);

            this.Id = id;
            this.StatId = statId;
            this.Value = value;
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
        public double Value
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
        public static IPercentageEnchantmentStore Create(
            Guid id,
            Guid statId,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IPercentageEnchantmentStore>() != null);

            return new PercentageEnchantmentStore(
                id,
                statId,
                value);
        }
        #endregion
    }
}
