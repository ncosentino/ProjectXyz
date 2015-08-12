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

        public IEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id, 
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId, 
            double minimumValue, 
            double maximumValue, 
            TimeSpan minimumDuration, 
            TimeSpan maximumDuration)
        {
            return PercentageEnchantmentDefinition.Create(
                id,
                statId,
                triggerId,
                statusTypeId,
                minimumValue,
                maximumValue,
                minimumDuration,
                maximumDuration);
        }
        #endregion
    }
}
