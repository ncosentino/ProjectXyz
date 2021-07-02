using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public interface ISkillEffectBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> EffectExecutors { get; }
    }
}
