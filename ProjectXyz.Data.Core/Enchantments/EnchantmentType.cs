using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentType : IEnchantmentType
    {
        #region Fields
        private readonly Guid _id;
        private readonly string _storeRepositoryClassName;
        private readonly string _definitionRepositoryClassName;
        #endregion

        #region Constructors
        private EnchantmentType(
            Guid id,
            string storeRepositoryClassName,
            string definitionRepositoryClassName)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(storeRepositoryClassName));
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(definitionRepositoryClassName));

            _id = id;
            _storeRepositoryClassName = storeRepositoryClassName;
            _definitionRepositoryClassName = definitionRepositoryClassName;
        }
        #endregion

        #region Properties
        public Guid Id 
        {
            get { return _id; }
        }

        public string StoreRepositoryClassName
        {
            get { return _storeRepositoryClassName; }
        }

        public string DefinitionRepositoryClassName
        {
            get { return _definitionRepositoryClassName; }
        }
        #endregion

        #region Methods
        public static IEnchantmentType Create(
            Guid id,
            string storeRepositoryClassName,
            string definitionRepositoryClassName)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(storeRepositoryClassName));
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(definitionRepositoryClassName));
            Contract.Ensures(Contract.Result<IEnchantmentType>() != null);

            return new EnchantmentType(
                id, 
                storeRepositoryClassName, 
                definitionRepositoryClassName);
        }
        #endregion
    }
}
