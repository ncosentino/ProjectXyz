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

        public IOneShotNegateEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id, 
            Guid statId)
        {
            return OneShotNegateEnchantmentDefinition.Create(
                id,
                statId);
        }
        #endregion
    }
}
