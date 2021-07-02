using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class SkillEffectBehavior :
        BaseBehavior,
        ISkillEffectBehavior
    {
        public SkillEffectBehavior(
            IEnumerable<IGameObject> effectExecutors)
        {
            EffectExecutors = effectExecutors.ToArray();
        }

        public IReadOnlyCollection<IGameObject> EffectExecutors { get; }
    }
}
