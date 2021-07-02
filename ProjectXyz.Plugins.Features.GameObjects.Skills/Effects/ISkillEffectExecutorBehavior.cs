using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public interface ISkillEffectExecutorBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> Effects { get; }
    }
}
