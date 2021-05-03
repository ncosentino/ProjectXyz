using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherDurationBehavior :
        BaseBehavior,
        IWeatherDuration
    {
        public WeatherDurationBehavior(
            double durationInTurns,
            IInterval transitionInDuration,
            IInterval transitionOutDuration)
        {
            DurationInTurns = durationInTurns;
            TransitionInDuration = transitionInDuration;
            TransitionOutDuration = transitionOutDuration;
        }

        public double DurationInTurns { get; }

        public IInterval TransitionInDuration { get; }

        public IInterval TransitionOutDuration { get; }
    }
}