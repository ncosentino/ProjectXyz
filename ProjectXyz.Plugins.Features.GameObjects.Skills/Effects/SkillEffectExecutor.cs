
using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class SkillEffectExecutor : ISkillEffectExecutor
    {
        public SkillEffectExecutor(
            bool isParallel,
            IEnumerable<ISkillEffectDefinition> effectDefinitions)
        {
            IsParallel = isParallel;
            EffectDefinitions = effectDefinitions;
        }

        public bool IsParallel { get; }

        public IEnumerable<ISkillEffectDefinition> EffectDefinitions { get; }
    }
}
