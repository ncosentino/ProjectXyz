using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public class EnchantmentDefinitionFactory : IEnchantmentDefinitionFactory
    {
        #region Constructors
        private EnchantmentDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentDefinitionFactory Create()
        {
            Contract.Ensures(Contract.Result<IEnchantmentDefinitionFactory>() != null);

            return new EnchantmentDefinitionFactory();
        }

        public IEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id, 
            Guid statId, 
            Guid calculationId, 
            Guid triggerId, 
            Guid statusTypeId, 
            double minimumValue, 
            double maximumValue, 
            TimeSpan minimumDuration, 
            TimeSpan maximumDuration)
        {
            return EnchantmentDefinition.Create(
                id,
                statId,
                calculationId,
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
