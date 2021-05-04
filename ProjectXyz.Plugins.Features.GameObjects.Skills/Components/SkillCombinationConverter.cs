using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class SkillCombinationGeneratorComponent : IGeneratorComponent
    {
        public SkillCombinationGeneratorComponent(
            params ISkillExecutorGeneratorComponent[] executorComponents)
        {
            Executors = executorComponents.ToArray();
        }

        public IReadOnlyCollection<ISkillExecutorGeneratorComponent> Executors { get; }
    }

    public sealed class SkillCombinationConverter : IBehaviorConverter
    {
        private readonly IReadOnlyCollection<IExecutorBehaviorConverter> _executorConverters;

        public SkillCombinationConverter(
            IEnumerable<IExecutorBehaviorConverter> executorConverters)
        {
            _executorConverters = executorConverters.ToArray();
        }

        public bool CanConvert(IGeneratorComponent component)
        {
            return component is SkillCombinationGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var comboComponent = (SkillCombinationGeneratorComponent)component;

            var executorBehaviors = new List<ISkillExecutorBehavior>();
            foreach (var executor in comboComponent.Executors)
            {
                var converter = _executorConverters.FirstOrDefault(x => x.CanConvert(executor));
                if (converter == null)
                {
                    continue;
                }

                executorBehaviors.Add(
                    converter.ConvertToBehavior(executor));
            }

            return new CombinationSkillBehavior(executorBehaviors);
        }
    }
}
