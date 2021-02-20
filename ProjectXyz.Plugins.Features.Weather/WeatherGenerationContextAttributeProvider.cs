using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherGenerationContextAttributeProvider : IDiscoverableFilterContextAttributeProvider
    {
        private readonly IReadOnlyWeatherManager _readOnlyWeatherManager;

        public WeatherGenerationContextAttributeProvider(IReadOnlyWeatherManager readOnlyWeatherManager)
        {
            _readOnlyWeatherManager = readOnlyWeatherManager;
        }

        public IEnumerable<IFilterAttribute> GetAttributes()
        {
            yield return new FilterAttribute(
                new StringIdentifier("weather"),
                new IdentifierFilterAttributeValue(_readOnlyWeatherManager.WeatherId),
                false);
        }
    }
}