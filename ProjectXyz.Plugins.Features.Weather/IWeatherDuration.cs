using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherDuration : IBehavior
    {
        double DurationInTurns { get; }

        IInterval TransitionInDuration { get; }

        IInterval TransitionOutDuration { get; }
    }
}