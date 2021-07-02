using System.Collections.Generic;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public interface ISkillEffectExecutor
    {
        bool IsParallel { get; }

        IEnumerable<ISkillEffectDefinition> EffectDefinitions { get; }
    }
}
