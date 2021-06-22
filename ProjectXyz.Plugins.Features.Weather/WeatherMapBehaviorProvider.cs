using System;
using System.Collections.Generic;
using System.Globalization;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Mapping;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherMapBehaviorProvider : IDiscoverableMapBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            if (!baseBehaviors.TryGetFirst<IReadOnlyMapPropertiesBehavior>(out var mapPropertiesBehavior))
            {
                yield break;
            }

            if (!mapPropertiesBehavior.Properties.TryGetValue(
                "WeatherTableId",
                out var rawWeatherTableId))
            {
                yield break;
            }

            var weatherTableId = new StringIdentifier(Convert.ToString(
                rawWeatherTableId,
                CultureInfo.InvariantCulture));
            yield return new MapWeatherTableBehavior(weatherTableId);
        }
    }
}
