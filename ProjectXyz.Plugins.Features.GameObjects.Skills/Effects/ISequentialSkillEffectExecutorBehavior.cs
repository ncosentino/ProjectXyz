using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public interface ISequentialSkillEffectExecutorBehavior : ISkillEffectExecutorBehavior
    {
    }

    public sealed class SequentialSkillEffectExecutorBehavior :
        BaseBehavior,
        ISequentialSkillEffectExecutorBehavior
    {
        public SequentialSkillEffectExecutorBehavior(
            IEnumerable<IGameObject> effects)
        {
            Effects = effects.ToArray();
        }

        public IReadOnlyCollection<IGameObject> Effects { get; }
    }
}
