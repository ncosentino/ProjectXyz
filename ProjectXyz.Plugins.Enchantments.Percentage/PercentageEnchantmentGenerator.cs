using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Percentage
{
    public sealed class PercentageEnchantmentGenerator : IPercentageEnchantmentGenerator
    {
        #region Fields
        private readonly IPercentageEnchantmentFactory _percentageEnchantmentFactory;
        private readonly IEnchantmentWeatherRepository _enchantmentWeatherRepository;
        #endregion

        #region Constructors
        private PercentageEnchantmentGenerator(
            IPercentageEnchantmentFactory percentageEnchantmentFactory,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            _percentageEnchantmentFactory = percentageEnchantmentFactory;
            _enchantmentWeatherRepository = enchantmentWeatherRepository;
        }
        #endregion

        #region Methods
        public static IPercentageEnchantmentGenerator Create(
            IPercentageEnchantmentFactory percentageEnchantmentFactory,
            IEnchantmentWeatherRepository enchantmentWeatherRepository)
        {
            var generator = new PercentageEnchantmentGenerator(
                percentageEnchantmentFactory,
                enchantmentWeatherRepository);
            return generator;
        }

        public IPercentageEnchantment Generate(
            IRandom randomizer, 
            IEnchantmentDefinition enchantmentDefinition,
            IPercentageEnchantmentDefinition percentageEnchantmentDefinition)
        {
            var value =
                percentageEnchantmentDefinition.MinimumValue +
                randomizer.NextDouble() * (percentageEnchantmentDefinition.MaximumValue - percentageEnchantmentDefinition.MinimumValue);
            var duration = TimeSpan.FromMilliseconds(
                percentageEnchantmentDefinition.MinimumDuration.TotalMilliseconds +
                randomizer.NextDouble() * (percentageEnchantmentDefinition.MaximumDuration.TotalMilliseconds - percentageEnchantmentDefinition.MinimumDuration.TotalMilliseconds));

            var enchantmentWeather = _enchantmentWeatherRepository.GetById(enchantmentDefinition.EnchantmentWeatherId);

            return _percentageEnchantmentFactory.Create(
                Guid.NewGuid(),
                enchantmentDefinition.StatusTypeId,
                enchantmentDefinition.TriggerId,
                enchantmentWeather.WeatherIds,
                duration,
                percentageEnchantmentDefinition.StatId,
                value);
        }
        #endregion
    }
}
