using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.Weather.Api
{
    public interface IWeatherBehaviorsInterceptor
    {
        void Intercept(IReadOnlyCollection<IBehavior> behaviors);
    }
}