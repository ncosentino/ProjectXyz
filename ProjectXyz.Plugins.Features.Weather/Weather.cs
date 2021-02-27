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
            IInterval duration,
            IInterval transitionInDuration,
            IInterval transitionOutDuration,
            IEnumerable<IBehavior> behaviors)
        {
            Duration = duration;
            TransitionInDuration = transitionInDuration;
            TransitionOutDuration = transitionOutDuration;
            Behaviors = behaviors.ToArray();
        }

        public IInterval Duration { get; }

        public IInterval TransitionInDuration { get; }

        public IInterval TransitionOutDuration { get; }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}