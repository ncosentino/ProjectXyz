using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentSaver : IEnchantmentSaver
    {
        #region Fields
        private readonly Dictionary<Type, SaveEnchantmentDelegate> _saveEnchantmentMapping;
        #endregion

        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="EnchantmentSaver"/> class from being created.
        /// </summary>
        private EnchantmentSaver()
        {
            _saveEnchantmentMapping = new Dictionary<Type, SaveEnchantmentDelegate>();
        }
        #endregion

        #region Methods
        public static IEnchantmentSaver Create()
        {
            Contract.Ensures(Contract.Result<IEnchantmentSaver>() != null);
            
            return new EnchantmentSaver();
        }

        /// <inheritdoc />
        public void RegisterCallbackForType<TSpecificType>(SaveEnchantmentDelegate callbackToRegister) 
            where TSpecificType : IEnchantmentStore
        {
            RegisterCallbackForType(typeof(TSpecificType), callbackToRegister);
        }

        /// <inheritdoc />
        public void RegisterCallbackForType(Type type, SaveEnchantmentDelegate callbackToRegister)
        {
            _saveEnchantmentMapping[type] = callbackToRegister;
        }

        /// <inheritdoc />
        public IEnchantmentStore Save(IEnchantment source)
        {
            var enchantmentType = source.GetType();
            if (!_saveEnchantmentMapping.ContainsKey(enchantmentType))
            {
                throw new InvalidOperationException(string.Format("No callback registered for type '{0}'.", enchantmentType));
            }

            var enchantmentStore = _saveEnchantmentMapping[enchantmentType].Invoke(source);
            return enchantmentStore;
        }
        #endregion


    }
}
