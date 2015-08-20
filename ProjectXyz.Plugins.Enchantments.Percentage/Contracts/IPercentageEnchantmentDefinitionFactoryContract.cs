using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Percentage.Contracts
{
    [ContractClassFor(typeof(IPercentageEnchantmentDefinitionFactory))]
    public abstract class IPercentageEnchantmentDefinitionFactoryContract : IPercentageEnchantmentDefinitionFactory
    {
        #region Methods
        public IPercentageEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id, 
            Guid statId, 
            double minimumValue,
            double maximumValue,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumValue <= maximumValue);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(maximumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration <= maximumDuration);
            Contract.Ensures(Contract.Result<IPercentageEnchantmentDefinition>() != null);

            return default(IPercentageEnchantmentDefinition);
        }
        #endregion
    }
}
