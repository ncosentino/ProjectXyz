using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentTypeFactory : IEnchantmentTypeFactory
    {
        #region Constructors
        private EnchantmentTypeFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeFactory Create()
        {
            var factory = new EnchantmentTypeFactory();
            return factory;
        }

        public IEnchantmentType Create(
            Guid id,
            string storeRepositoryClassName,
            string definitionRepositoryClassName)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(storeRepositoryClassName));
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(definitionRepositoryClassName));
            Contract.Ensures(Contract.Result<IEnchantmentType>() != null);

            return EnchantmentType.Create(
                id, 
                storeRepositoryClassName, 
                definitionRepositoryClassName);
        }
        #endregion
    }
}
