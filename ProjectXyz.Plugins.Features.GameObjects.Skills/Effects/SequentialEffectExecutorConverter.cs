using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Effects;

using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class SequentialEffectExecutorGeneratorComponent : ISkillEffectExecutorGeneratorComponent
    {
        public SequentialEffectExecutorGeneratorComponent(
            IEnumerable<ISkillEffectDefinition> effectDefinitions)
        {
            EffectDefinitions = effectDefinitions.ToArray();
        }

        public IReadOnlyCollection<ISkillEffectDefinition> EffectDefinitions { get; }
    }

    public sealed class SequentialEffectExecutorConverter : ISkillEffectExecutorBehaviorConverter
    {
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IReadOnlyCollection<ISkillEffectBehaviorConverter> _executorConverters;

        public SequentialEffectExecutorConverter(
            IGameObjectFactory gameObjectFactory,
            IEnumerable<ISkillEffectBehaviorConverter> executorConverters)
        {
            _gameObjectFactory = gameObjectFactory;
            _executorConverters = executorConverters.ToArray();
        }

        public bool CanConvert(IGeneratorComponent component)
        {
            return component is SequentialEffectExecutorGeneratorComponent;
        }

        public IGameObject ConvertToExecutor(
            IGeneratorComponent component)
        {
            var sequentialComponent = (SequentialEffectExecutorGeneratorComponent)component;

            var effects = new List<IGameObject>();
            foreach (var effectDefinition in sequentialComponent.EffectDefinitions)
            {
                var effectBehaviors = new List<IBehavior>();
                foreach (var effectComponent in effectDefinition.FilterComponents)
                {
                    var converter = _executorConverters.FirstOrDefault(x => x.CanConvert(effectComponent));
                    if (converter == null)
                    {
                        continue;
                    }

                    effectBehaviors.Add(converter.ConvertToBehavior(effectComponent));
                }

                var effect = _gameObjectFactory.Create(effectBehaviors);

                effects.Add(effect);
            }

            return _gameObjectFactory.Create(new[]
            {
                new SequentialSkillEffectExecutorBehavior(effects)
            });
        }
    }
}
