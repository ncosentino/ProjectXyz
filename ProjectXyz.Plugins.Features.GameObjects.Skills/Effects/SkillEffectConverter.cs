using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class SkillEffectGeneratorComponent : IGeneratorComponent
    {
        public SkillEffectGeneratorComponent(
            params ISkillEffectExecutorGeneratorComponent[] executorComponents)
        {
            Executors = executorComponents.ToArray();
        }

        public IReadOnlyCollection<ISkillEffectExecutorGeneratorComponent> Executors { get; }
    }

    public sealed class SkillEffectConverter : IBehaviorConverter
    {
        private readonly IReadOnlyCollection<ISkillEffectExecutorBehaviorConverter> _executorConverters;

        public SkillEffectConverter(
            IEnumerable<ISkillEffectExecutorBehaviorConverter> executorConverters)
        {
            _executorConverters = executorConverters.ToArray();
        }

        public bool CanConvert(IGeneratorComponent component)
        {
            return component is SkillEffectGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var comboComponent = (SkillEffectGeneratorComponent)component;

            var executors = new List<IGameObject>();
            foreach (var executor in comboComponent.Executors)
            {
                var converter = _executorConverters.FirstOrDefault(x => x.CanConvert(executor));
                if (converter == null)
                {
                    continue;
                }

                executors.Add(converter.ConvertToExecutor(executor));
            }

            return new SkillEffectBehavior(executors);
        }
    }
}
