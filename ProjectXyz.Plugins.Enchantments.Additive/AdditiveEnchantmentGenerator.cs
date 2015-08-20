using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Additive
{
    public sealed class AdditiveEnchantmentGenerator : IAdditiveEnchantmentGenerator
    {
        #region Fields
        private readonly IAdditiveEnchantmentFactory _additiveEnchantmentFactory;
        private readonly IEnchantmentWeatherRepository _enchantmentWeatherRepository;
        #endregion

        #region Constructors
        private AdditiveEnchantmentGenerator(
            IAdditiveEnchantmentFactory additiveEnchantmentFactory,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            _additiveEnchantmentFactory = additiveEnchantmentFactory;
            _enchantmentWeatherRepository = enchantmentWeatherRepository;
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentGenerator Create(
            IAdditiveEnchantmentFactory additiveEnchantmentFactory,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            var generator = new AdditiveEnchantmentGenerator(
                additiveEnchantmentFactory,
                enchantmentWeatherRepository);
            return generator;
        }

        public IAdditiveEnchantment Generate(
            IRandom randomizer, 
            IEnchantmentDefinition enchantmentDefinition,
            IAdditiveEnchantmentDefinition additiveEnchantmentDefinition)
        {
            var value =
                additiveEnchantmentDefinition.MinimumValue +
                randomizer.NextDouble() * (additiveEnchantmentDefinition.MaximumValue - additiveEnchantmentDefinition.MinimumValue);
            var duration = TimeSpan.FromMilliseconds(
                additiveEnchantmentDefinition.MinimumDuration.TotalMilliseconds +
                randomizer.NextDouble() * (additiveEnchantmentDefinition.MaximumDuration.TotalMilliseconds - additiveEnchantmentDefinition.MinimumDuration.TotalMilliseconds));

            var enchantmentWeather = _enchantmentWeatherRepository.GetById(enchantmentDefinition.EnchantmentWeatherId);

            return _additiveEnchantmentFactory.Create(
                Guid.NewGuid(),
                enchantmentDefinition.StatusTypeId,
                enchantmentDefinition.TriggerId,
                enchantmentWeather.WeatherIds,
                duration,
                additiveEnchantmentDefinition.StatId,
                value);
        }
        #endregion
    }
}
