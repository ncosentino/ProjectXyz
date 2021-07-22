using System;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States;

namespace ProjectXyz.Plugins.Features.Weather.Default
{
    public sealed class WeatherManager : IWeatherManager
    {
        private readonly IWeatherIdentifiers _weatherIdentifiers;
        private readonly Lazy<IStateManager> _lazyStateManager;
        private IGameObject _weather;

        public WeatherManager(
            IWeatherIdentifiers weatherIdentifiers,
            Lazy<IStateManager> lazyStateManager)
        {
            _weatherIdentifiers = weatherIdentifiers;
            _lazyStateManager = lazyStateManager;
        }

        public IWeatherTable WeatherTable { get; set; }

        public IGameObject Weather 
        { 
            get => _weather; 
            set
            {
                _weather = value;
                _lazyStateManager.Value.SetState(
                    _weatherIdentifiers.WeatherStateTypeId,
                    _weatherIdentifiers.KindOfWeatherStateId,
                    value?.GetOnly<IReadOnlyIdentifierBehavior>().ToString());
            }
        }
    }
}
