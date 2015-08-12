using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public class AdditiveEnchantmentDefinitionFactory : IAdditiveEnchantmentDefinitionFactory
    {
        #region Constructors
        private AdditiveEnchantmentDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentDefinitionFactory Create()
        {
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentDefinitionFactory>() != null);

            return new AdditiveEnchantmentDefinitionFactory();
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
            return AdditiveEnchantmentDefinition.Create(
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
