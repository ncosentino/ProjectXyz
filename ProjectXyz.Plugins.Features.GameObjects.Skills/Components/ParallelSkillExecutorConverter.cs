using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class ParallelSkillExecutorGeneratorComponent : ISkillExecutorGeneratorComponent
    {
        public ParallelSkillExecutorGeneratorComponent(
            IEnumerable<IIdentifier> skillIdentifiers)
        {
            SkillIdentifiers = skillIdentifiers.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> SkillIdentifiers { get; }
    }

    public sealed class ParallelSkillExecutorConverter : IExecutorBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent component)
        {
            return component is ParallelSkillExecutorGeneratorComponent;
        }

        public ISkillExecutorBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var parallelComponent = (ParallelSkillExecutorGeneratorComponent)component;
            
            return new ParallelSkillExecutorBehavior(
                parallelComponent.SkillIdentifiers);
        }
    }
}
