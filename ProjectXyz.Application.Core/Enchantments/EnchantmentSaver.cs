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
        private readonly IEnchantmentWeatherFactory _enchantmentWeatherFactory;
        private readonly IEnchantmentWeatherRepository _enchantmentWeatherRepository;
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
        /// <param name="enchantmentWeatherFactory">The enchantment weather factory.</param>
        /// <param name="enchantmentWeatherRepository">The enchantment weather repository.</param>
        private EnchantmentSaver(
            IEnchantmentStoreFactory enchantmentStoreFactory,
            IEnchantmentStoreRepository enchantmentStoreRepository,
            IEnchantmentWeatherFactory enchantmentWeatherFactory,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStoreFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentStoreRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentWeatherFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentWeatherRepository != null);

            _saveEnchantmentMapping = new Dictionary<Type, SaveEnchantmentDelegate>();
            _enchantmentStoreFactory = enchantmentStoreFactory;
            _enchantmentStoreRepository = enchantmentStoreRepository;
            _enchantmentWeatherFactory = enchantmentWeatherFactory;
            _enchantmentWeatherRepository = enchantmentWeatherRepository;
        }
        #endregion

        #region Methods
        public static IEnchantmentSaver Create(
            IEnchantmentStoreFactory enchantmentStoreFactory,
            IEnchantmentStoreRepository enchantmentStoreRepository,
            IEnchantmentWeatherFactory enchantmentWeatherFactory,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            Contract.Requires<ArgumentNullException>(enchantmentStoreFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentStoreRepository != null);
            Contract.Requires<ArgumentNullException>(enchantmentWeatherFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentWeatherRepository != null);
            Contract.Ensures(Contract.Result<IEnchantmentSaver>() != null);

            return new EnchantmentSaver(
                enchantmentStoreFactory,
                enchantmentStoreRepository,
                enchantmentWeatherFactory,
                enchantmentWeatherRepository);
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

            var enchantmentWeather = _enchantmentWeatherFactory.Create(
                Guid.NewGuid(),
                source.Id,
                source.WeatherIds);
            // TODO: save/update?
            _enchantmentWeatherRepository.Add(enchantmentWeather);

            var enchantmentStore = _enchantmentStoreFactory.Create(
                source.Id,
                source.TriggerId,
                source.StatusTypeId,
                source.EnchantmentTypeId,
                enchantmentWeather.Id,
                source.RemainingDuration);

            // TODO: save/update?
            _enchantmentStoreRepository.Add(enchantmentStore);

            // FIXME: remove the return value;
            return null;
        }
        #endregion


    }
}
