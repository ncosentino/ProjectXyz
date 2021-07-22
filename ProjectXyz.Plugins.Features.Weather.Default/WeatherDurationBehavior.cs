using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.Weather.Default
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