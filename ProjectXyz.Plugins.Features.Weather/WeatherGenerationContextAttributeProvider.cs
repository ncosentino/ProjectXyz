using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherGenerationContextAttributeProvider : IGeneratorContextAttributeProvider
    {
        private readonly IWeatherSystem _weatherSystem;

        public WeatherGenerationContextAttributeProvider(IWeatherSystem weatherSystem)
        {
            _weatherSystem = weatherSystem;
        }

        public IEnumerable<IGeneratorAttribute> GetAttributes()
        {
            yield return new GeneratorAttribute(
                new StringIdentifier("weather"),
                new IdentifierGeneratorAttributeValue(_weatherSystem.Weather));
        }
    }
}