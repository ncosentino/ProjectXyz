using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public interface IParallelSkillEffectExecutorBehavior : ISkillEffectExecutorBehavior
    {
    }

    public sealed class ParallelSkillEffectExecutorBehavior :
        BaseBehavior,
        IParallelSkillEffectExecutorBehavior
    {
        public ParallelSkillEffectExecutorBehavior(
            IEnumerable<IGameObject> effects)
        {
            Effects = effects.ToArray();
        }

        public IReadOnlyCollection<IGameObject> Effects { get; }
    }
}
