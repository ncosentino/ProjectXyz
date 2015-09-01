using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentSaver : IEnchantmentSaver
    {
        #region Fields
        private readonly IEnchantmentStoreFactory _enchantmentStoreFactory;
        private readonly IEnchantmentStoreRepository _enchantmentStoreRepository;
        private readonly Dictionary<Type, SaveEnchantmentDelegate> _saveEnchantmentMapping;
        #endregion

        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="EnchantmentSaver"/> class from being created.
        /// </summary>
        /// <param name="enchantmentStoreFactory">The enchantment store factory.</param>
        /// <param name="enchantmentStoreRepository">The enchantment store repository.</param>
        private EnchantmentSaver(
            IEnchantmentStoreFactory enchantmentStoreFactory,
            IEnchantmentStoreRepository enchantmentStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStoreFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentStoreRepository != null);

            _saveEnchantmentMapping = new Dictionary<Type, SaveEnchantmentDelegate>();
            _enchantmentStoreFactory = enchantmentStoreFactory;
            _enchantmentStoreRepository = enchantmentStoreRepository;
        }
        #endregion

        #region Methods
        public static IEnchantmentSaver Create(
            IEnchantmentStoreFactory enchantmentStoreFactory,
            IEnchantmentStoreRepository enchantmentStoreRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStoreFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentStoreRepository != null);
            Contract.Ensures(Contract.Result<IEnchantmentSaver>() != null);

            return new EnchantmentSaver(
                enchantmentStoreFactory,
                enchantmentStoreRepository);
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

            _saveEnchantmentMapping[enchantmentType].Invoke(source);
            
            var enchantmentStore = _enchantmentStoreFactory.Create(
                source.Id,
                source.TriggerId,
                source.StatusTypeId,
                source.EnchantmentTypeId,
                source.WeatherGroupingId);

            // TODO: save/update?
            _enchantmentStoreRepository.Add(enchantmentStore);

            // FIXME: remove the return value;
            return null;
        }
        #endregion


    }
}
