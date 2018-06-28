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
        //
        // TODO: weather shouldn't be random but... just for fun
        //
        private static readonly IInterval<double> ARBITRARY_WEATHER_SWITCH_PERIOD = new Interval<double>(10000);

        private readonly IWeatherManager _weatherManager;
        private IInterval _currentCycleTime;

        public WeatherSystem(IWeatherManager weatherManager)
        {
            _weatherManager = weatherManager;
            _currentCycleTime = new Interval<double>(0);
        }

        public void Update(
            ISystemUpdateContext systemUpdateContext,
            IEnumerable<IHasBehaviors> hasBehaviors)
        {
            var elapsed = systemUpdateContext
                .GetFirst<IComponent<IElapsedTime>>()
                .Value
                .Interval;
            _currentCycleTime = _currentCycleTime.Add(elapsed);

            if (((IInterval<double>)_currentCycleTime).Value >= ARBITRARY_WEATHER_SWITCH_PERIOD.Value)
            {
                //
                // TODO: actually calculate the weather
                //
                _weatherManager.WeatherId = Equals(_weatherManager.WeatherId, WeatherIds.Rain)
                    ? WeatherIds.Clear
                    : WeatherIds.Rain;

                _currentCycleTime = new Interval<double>(0);
            }
        }
    }
}