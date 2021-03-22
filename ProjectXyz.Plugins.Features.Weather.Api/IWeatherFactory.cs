using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeatherFactory
    {
        IWeather Create(
            IIdentifier weatherId,
            double durationInTurns,
            IInterval transitionInDuration,
            IInterval transitionOutDuration,
            IEnumerable<IBehavior> additionalBehaviors);
    }
}