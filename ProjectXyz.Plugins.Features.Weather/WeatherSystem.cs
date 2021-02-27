using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherSystem : IWeatherSystem
    {
        private readonly IWeatherFactory _weatherFactory;
        private readonly IWeatherManager _weatherManager;
        private readonly IRandom _random;

        private IWeatherTable _currentWeatherTable;
        private IInterval _currentCycleTime;
        private IInterval _targetCycleTime;

        public WeatherSystem(
            IWeatherManager weatherManager,
            IWeatherFactory weatherFactory,
            IRandom random)
        {
            _weatherManager = weatherManager;
            _weatherFactory = weatherFactory;
            _random = random;
            _currentCycleTime = new Interval<double>(0);
            _targetCycleTime = new Interval<double>(0);
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

            // FIXME: we gotta do better than this casting...
            if (((IInterval<double>)_currentCycleTime).Value >=
                ((IInterval<double>)_targetCycleTime).Value)
            {
                _currentCycleTime = new Interval<double>(0);

                // pull in the current weather table from the manager
                _currentWeatherTable = _weatherManager.WeatherTable;
                if (_currentWeatherTable == null)
                {
                    _weatherManager.Weather = null;
                    return;
                }

                var nextWeatherEntry = GetNextWeatherEntry(
                    _currentWeatherTable,
                    _random);

                // FIXME: we gotta do better than this casting...
                _targetCycleTime = new Interval<double>(_random.NextDouble(
                    ((IInterval<double>)nextWeatherEntry.MinimumDuration).Value,
                    ((IInterval<double>)nextWeatherEntry.MaximumDuration).Value));
                _weatherManager.Weather = _weatherFactory.Create(
                    nextWeatherEntry.WeatherId,
                    _targetCycleTime,
                    new Interval<double>(5000), // FIXME: load transition from table??
                    new Interval<double>(5000), // FIXME: load transition from table??
                    new IBehavior[] { });
            }
        }

        private IWeightedWeatherTableEntry GetNextWeatherEntry(
            IWeatherTable weatherTable,
            IRandom random)
        {
            var totalWeight = weatherTable
                .WeightedEntries
                .Sum(x => x.Weight);
            var randomRoll = random.NextDouble(0, totalWeight);
            var weightAccumulator = 0d;
            foreach (var entry in weatherTable.WeightedEntries)
            {
                weightAccumulator += entry.Weight;
                if (weightAccumulator >= randomRoll)
                {
                    return entry;
                }
            }

            throw new InvalidOperationException(
                $"The method '{nameof(GetNextWeatherEntry)}' should always " +
                $"select a '{typeof(IWeightedWeatherTableEntry)}'.");
        }
    }
}