using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public sealed class EnchantmentPlugin : IEnchantmentPlugin
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _enchantmentTypeId;
        private readonly string _storeRepositoryClassName;
        private readonly string _definitionRepositoryClassName;
        #endregion

        #region Constructors
        private EnchantmentPlugin(
            Guid id,
            Guid enchantmentTypeId,
            string storeRepositoryClassName,
            string definitionRepositoryClassName)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(storeRepositoryClassName));
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(definitionRepositoryClassName));

            _id = id;
            _enchantmentTypeId = enchantmentTypeId;
            _storeRepositoryClassName = storeRepositoryClassName;
            _definitionRepositoryClassName = definitionRepositoryClassName;
        }
        #endregion

        #region Properties
        public Guid Id 
        {
            get { return _id; }
        }

        public Guid EnchantmentTypeId
        {
            get { return _enchantmentTypeId; }
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
        public static IEnchantmentPlugin Create(
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

            return new EnchantmentPlugin(
                id, 
                enchantmentTypeId,
                storeRepositoryClassName, 
                definitionRepositoryClassName);
        }
        #endregion
    }
}
