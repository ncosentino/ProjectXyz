using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentPluginFactory : IEnchantmentPluginFactory
    {
        #region Constructors
        private EnchantmentPluginFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentPluginFactory Create()
        {
            var factory = new EnchantmentPluginFactory();
            return factory;
        }

        public IEnchantmentPlugin Create(
            Guid id,
            Guid enchantmentTypeId,
            string storeRepositoryClassName,
            string definitionRepositoryClassName)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(storeRepositoryClassName));
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(definitionRepositoryClassName));
            Contract.Ensures(Contract.Result<IEnchantmentPlugin>() != null);

            return EnchantmentPlugin.Create(
                id,
                enchantmentTypeId,
                storeRepositoryClassName, 
                definitionRepositoryClassName);
        }
        #endregion
    }
}
