using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherGenerationContextAttributeProvider : IDiscoverableFilterContextAttributeProvider
    {
        private readonly IWeatherIdentifiers _weatherIdentifiers;
        private readonly IReadOnlyWeatherManager _readOnlyWeatherManager;

        public WeatherGenerationContextAttributeProvider(
            IReadOnlyWeatherManager readOnlyWeatherManager,
            IWeatherIdentifiers weatherIdentifiers)
        {
            _readOnlyWeatherManager = readOnlyWeatherManager;
            _weatherIdentifiers = weatherIdentifiers;
        }

        public IEnumerable<IFilterAttribute> GetAttributes()
        {
            if (_readOnlyWeatherManager.Weather != null)
            {
                var weatherId = _readOnlyWeatherManager
                    .Weather
                    .GetOnly<IIdentifierBehavior>().Id;
                yield return new FilterAttribute(
                    _weatherIdentifiers.FilterContextWeatherIdentifier,
                    new IdentifierFilterAttributeValue(weatherId),
                    false);
            }
        }
    }
}