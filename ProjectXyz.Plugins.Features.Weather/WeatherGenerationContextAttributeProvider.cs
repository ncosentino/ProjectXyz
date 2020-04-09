using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherGenerationContextAttributeProvider : IGeneratorContextAttributeProvider
    {
        private readonly IReadOnlyWeatherManager _readOnlyWeatherManager;

        public WeatherGenerationContextAttributeProvider(IReadOnlyWeatherManager readOnlyWeatherManager)
        {
            _readOnlyWeatherManager = readOnlyWeatherManager;
        }

        public IEnumerable<IGeneratorAttribute> GetAttributes()
        {
            yield return new GeneratorAttribute(
                new StringIdentifier("weather"),
                new IdentifierGeneratorAttributeValue(_readOnlyWeatherManager.WeatherId),
                false);
        }
    }
}