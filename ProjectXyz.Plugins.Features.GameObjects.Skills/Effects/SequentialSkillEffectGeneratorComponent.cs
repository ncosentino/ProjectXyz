using ProjectXyz.Api.GameObjects.Generation;

using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class SequentialSkillEffectGeneratorComponent : IGeneratorComponent
    {
        public SequentialSkillEffectGeneratorComponent(
            IEnumerable<ISkillEffectDefinition> skillEffectDefinition)
        {
            SkillEffectDefinition = skillEffectDefinition.ToArray();
        }

        public IEnumerable<ISkillEffectDefinition> SkillEffectDefinition { get; }
    }
}
