using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherSystem : IWeatherSystem
    {
        private readonly IWeatherManager _weatherManager;
        private IInterval _current;

        public WeatherSystem(IWeatherManager weatherManager)
        {
            _weatherManager = weatherManager;
            _current = new Interval<double>(0);
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            _current = _current.Add(elapsed);

            // TODO: actually calculate the weather
            _weatherManager.WeatherId = WeatherIds.Rain;
        }
    }
}