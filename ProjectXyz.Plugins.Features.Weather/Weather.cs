using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class Weather : IWeather
    {
        public Weather(
            double durationInTurns,
            IInterval transitionInDuration,
            IInterval transitionOutDuration,
            IEnumerable<IBehavior> behaviors)
        {
            DurationInTurns = durationInTurns;
            TransitionInDuration = transitionInDuration;
            TransitionOutDuration = transitionOutDuration;
            Behaviors = behaviors.ToArray();
        }

        public double DurationInTurns { get; }

        public IInterval TransitionInDuration { get; }

        public IInterval TransitionOutDuration { get; }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}