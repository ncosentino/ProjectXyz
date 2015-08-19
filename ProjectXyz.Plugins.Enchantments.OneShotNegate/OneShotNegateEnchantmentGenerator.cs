using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentGenerator : IOneShotNegateEnchantmentGenerator
    {
        #region Fields
        private readonly IOneShotNegateEnchantmentFactory _oneShotNegateEnchantmentFactory;
        private readonly IEnchantmentWeatherRepository _enchantmentWeatherRepository;
        #endregion

        #region Constructors
        private OneShotNegateEnchantmentGenerator(
            IOneShotNegateEnchantmentFactory oneShotNegateEnchantmentFactory,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            _oneShotNegateEnchantmentFactory = oneShotNegateEnchantmentFactory;
            _enchantmentWeatherRepository = enchantmentWeatherRepository;
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentGenerator Create(
            IOneShotNegateEnchantmentFactory oneShotNegateEnchantmentFactory,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            var generator = new OneShotNegateEnchantmentGenerator(
                oneShotNegateEnchantmentFactory,
                enchantmentWeatherRepository);
            return generator;
        }

        public IOneShotNegateEnchantment Generate(
            IRandom randomizer, 
            IEnchantmentDefinition enchantmentDefinition,
            IOneShotNegateEnchantmentDefinition oneShotNegateEnchantmentDefinition)
        {
            var enchantmentWeather = _enchantmentWeatherRepository.GetById(enchantmentDefinition.EnchantmentWeatherId);

            return _oneShotNegateEnchantmentFactory.Create(
                Guid.NewGuid(),
                enchantmentDefinition.StatusTypeId,
                enchantmentDefinition.TriggerId,
                enchantmentWeather.WeatherIds,
                oneShotNegateEnchantmentDefinition.StatId);
        }
        #endregion
    }
}
