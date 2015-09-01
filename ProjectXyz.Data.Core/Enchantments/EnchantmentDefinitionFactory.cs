using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentDefinitionFactory : IEnchantmentDefinitionFactory
    {
        #region Constructors
        private EnchantmentDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentDefinitionFactory Create()
        {
            var factory = new EnchantmentDefinitionFactory();
            return factory;
        }

        public IEnchantmentDefinition Create(
            Guid id,
            Guid triggerId,
            Guid statusTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnchantmentDefinition>() != null);

            var enchantmentDefinition = EnchantmentDefinition.Create(
                id,
                triggerId,
                statusTypeId);
            return enchantmentDefinition;
        }
        #endregion
    }
}
