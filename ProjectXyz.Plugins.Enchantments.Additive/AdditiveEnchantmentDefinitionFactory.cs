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

        public IAdditiveEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id, 
            Guid statId, 
            double minimumValue, 
            double maximumValue)
        {
            return AdditiveEnchantmentDefinition.Create(
                id,
                statId,
                minimumValue,
                maximumValue);
        }
        #endregion
    }
}
