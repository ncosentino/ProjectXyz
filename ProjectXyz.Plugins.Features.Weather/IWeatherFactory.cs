using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Weather
{
    public interface IWeatherFactory
    {
        IGameObject Create(
            IIdentifier weatherId,
            IWeatherDuration weatherDuration,
            IEnumerable<IBehavior> additionalBehaviors);
    }
}