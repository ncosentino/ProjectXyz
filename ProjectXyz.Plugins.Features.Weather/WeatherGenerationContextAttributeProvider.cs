using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
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
            if (_readOnlyWeatherManager.Weather != null)
            {
                var weatherId = _readOnlyWeatherManager
                    .Weather
                    .GetOnly<IIdentifierBehavior>().Id;
                yield return new FilterAttribute(
                    new StringIdentifier("weather"),
                    new IdentifierFilterAttributeValue(weatherId),
                    false);
            }
        }
    }
}