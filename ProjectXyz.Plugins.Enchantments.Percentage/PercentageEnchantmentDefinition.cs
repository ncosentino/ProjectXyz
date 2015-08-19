using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public sealed class PercentageEnchantmentDefinition : IPercentageEnchantmentDefinition
    {
        #region Constructors
        private PercentageEnchantmentDefinition(
            Guid id,
            Guid statId,
            double minimumValue,
            double maximumValue)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);

            this.Id = id;
            this.StatId = statId;
            this.MinimumValue = minimumValue;
            this.MaximumValue = maximumValue;
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
        
        public double MinimumValue
        {
            get;
            set;
        }

        public double MaximumValue
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static IPercentageEnchantmentDefinition Create(
            Guid id,
            Guid statId,
            double minimumValue,
            double maximumValue)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);
            Contract.Ensures(Contract.Result<IPercentageEnchantmentDefinition>() != null);

            var enchantmentDefinition = new PercentageEnchantmentDefinition(
                id,
                statId,
                minimumValue,
                maximumValue);
            return enchantmentDefinition;
        }
        #endregion
    }
}
