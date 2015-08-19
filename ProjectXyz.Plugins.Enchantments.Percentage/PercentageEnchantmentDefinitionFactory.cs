using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public class PercentageEnchantmentDefinitionFactory : IPercentageEnchantmentDefinitionFactory
    {
        #region Constructors
        private PercentageEnchantmentDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IPercentageEnchantmentDefinitionFactory Create()
        {
            Contract.Ensures(Contract.Result<IPercentageEnchantmentDefinitionFactory>() != null);

            return new PercentageEnchantmentDefinitionFactory();
        }

        public IPercentageEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id, 
            Guid statId, 
            double minimumValue, 
            double maximumValue)
        {
            return PercentageEnchantmentDefinition.Create(
                id,
                statId,
                minimumValue,
                maximumValue);
        }
        #endregion
    }
}
