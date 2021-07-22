using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes; // FIXME: dependency on non-API

namespace ProjectXyz.Plugins.Features.Weather.Default
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