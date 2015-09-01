using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Weather;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentGenerator : IOneShotNegateEnchantmentGenerator
    {
        #region Fields
        private readonly IOneShotNegateEnchantmentFactory _oneShotNegateEnchantmentFactory;
        private readonly IEnchantmentDefinitionWeatherTypeGroupingRepository _enchantmentDefinitionWeatherGroupingRepository;
        private readonly IWeatherGroupingRepository _weatherGroupingRepository;
        #endregion

        #region Constructors
        private OneShotNegateEnchantmentGenerator(
            IOneShotNegateEnchantmentFactory oneShotNegateEnchantmentFactory,
            IEnchantmentDefinitionWeatherTypeGroupingRepository enchantmentDefinitionWeatherGroupingRepository)
        {
            _oneShotNegateEnchantmentFactory = oneShotNegateEnchantmentFactory;
            _enchantmentDefinitionWeatherGroupingRepository = enchantmentDefinitionWeatherGroupingRepository;
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentGenerator Create(
            IOneShotNegateEnchantmentFactory oneShotNegateEnchantmentFactory,
            IEnchantmentDefinitionWeatherTypeGroupingRepository enchantmentDefinitionWeatherGroupingRepository)
        {
            var generator = new OneShotNegateEnchantmentGenerator(
                oneShotNegateEnchantmentFactory,
                enchantmentDefinitionWeatherGroupingRepository);
            return generator;
        }

        public IOneShotNegateEnchantment Generate(
            IRandom randomizer, 
            IEnchantmentDefinition enchantmentDefinition,
            IOneShotNegateEnchantmentDefinition oneShotNegateEnchantmentDefinition)
        {
            var enchantmentWeather = _enchantmentDefinitionWeatherGroupingRepository.GetByEnchantmentDefinitionId(enchantmentDefinition.Id);
            
            return _oneShotNegateEnchantmentFactory.Create(
                Guid.NewGuid(),
                enchantmentDefinition.StatusTypeId,
                enchantmentDefinition.TriggerId,
                enchantmentWeather.WeatherTypeGroupingId,
                oneShotNegateEnchantmentDefinition.StatId);
        }
        #endregion
    }
}
