using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public class OneShotNegateEnchantmentDefinitionFactory : IOneShotNegateEnchantmentDefinitionFactory
    {
        #region Constructors
        private OneShotNegateEnchantmentDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentDefinitionFactory Create()
        {
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentDefinitionFactory>() != null);

            return new OneShotNegateEnchantmentDefinitionFactory();
        }

        public IEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id, 
            Guid statId, 
            Guid triggerId, 
            Guid statusTypeId)
        {
            return OneShotNegateEnchantmentDefinition.Create(
                id,
                statId,
                triggerId,
                statusTypeId);
        }
        #endregion
    }
}
