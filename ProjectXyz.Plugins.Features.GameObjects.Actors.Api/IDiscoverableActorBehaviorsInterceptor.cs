﻿namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IDiscoverableActorBehaviorsInterceptor : IActorBehaviorsInterceptor
    {
        int Priority { get; }
    }
}
