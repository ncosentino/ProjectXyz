using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IActorFactory
    {
        IGameObject Create(
            IReadOnlyIdentifierBehavior identifierBehavior,
            IEnumerable<IBehavior> additionalbehaviors);
    }
}