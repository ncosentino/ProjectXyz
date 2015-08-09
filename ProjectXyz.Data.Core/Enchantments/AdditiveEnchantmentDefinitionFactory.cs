using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
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
