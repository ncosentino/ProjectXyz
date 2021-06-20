using System;
using System.Linq;
using System.Threading.Tasks;

using NexusLabs.Framework;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.Systems;
using ProjectXyz.Plugins.Features.TurnBased.Api;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherSystem : IWeatherSystem
    {
        private readonly IWeatherFactory _weatherFactory;
        private readonly IWeatherManager _weatherManager;
        private readonly IWeatherModifiers _weatherModifiers;
        private readonly IRandom _random;

        private IWeatherTable _currentWeatherTable;
        private double _currentCycleTimeInTurns;
        private double _targetCycleTimeInTurns;

        public WeatherSystem(
            IWeatherManager weatherManager,
            IWeatherFactory weatherFactory,
            IWeatherModifiers weatherModifiers,
            IRandom random)
        {
            _weatherManager = weatherManager;
            _weatherFactory = weatherFactory;
            _weatherModifiers = weatherModifiers;
            _random = random;
        }

        public int? Priority => null;

        public async Task UpdateAsync(ISystemUpdateContext systemUpdateContext)
        {
            var elapsedTurns = systemUpdateContext
                .GetFirst<IComponent<ITurnInfo>>()
                .Value
                .ElapsedTurns;
            _currentCycleTimeInTurns += elapsedTurns;

            if (_currentCycleTimeInTurns >= _targetCycleTimeInTurns)
            {
                _currentCycleTimeInTurns = 0;

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

                var adjustedMaximumDuration = _weatherModifiers.GetMaximumDuration(
                    nextWeatherEntry.WeatherId,
                    nextWeatherEntry.MaximumDurationInTurns);
                var adjustedMinimumDuration = _weatherModifiers.GetMinimumDuration(
                    nextWeatherEntry.WeatherId,
                    nextWeatherEntry.MinimumDurationInTurns,
                    adjustedMaximumDuration);
                _targetCycleTimeInTurns = _random.NextDouble(
                    adjustedMinimumDuration,
                    adjustedMaximumDuration);

                _weatherManager.Weather = _weatherFactory.Create(
                    nextWeatherEntry.WeatherId,
                    new WeatherDurationBehavior(
                        _targetCycleTimeInTurns,
                        new Interval<double>(5000), // FIXME: load transition from table??
                        new Interval<double>(5000)), // FIXME: load transition from table??
                    new IBehavior[] { });
            }
        }

        private IWeightedWeatherTableEntry GetNextWeatherEntry(
            IWeatherTable weatherTable,
            IRandom random)
        {
            var adjustedWeights = _weatherModifiers.GetWeights(weatherTable
                .WeightedEntries
                .ToDictionary(
                    x => x.WeatherId,
                    x => x.Weight));
            var totalWeight = adjustedWeights.Sum(x => x.Value);
            var randomRoll = random.NextDouble(0, totalWeight);
            var weightAccumulator = 0d;
            foreach (var entry in weatherTable.WeightedEntries)
            {
                weightAccumulator += adjustedWeights[entry.WeatherId];
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