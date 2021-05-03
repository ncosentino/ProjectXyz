using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.Logging;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.Default
{
    public sealed class GeneratorComponentToBehaviorConverterFacade : IGeneratorComponentToBehaviorConverterFacade
    {
        private readonly Dictionary<Type, ConvertGeneratorComponentToBehaviorDelegate> _mapping;
        private readonly List<Tuple<Predicate<IGeneratorComponent>, ConvertGeneratorComponentToBehaviorDelegate>> _predicateMapping;

        public GeneratorComponentToBehaviorConverterFacade(
            ILogger logger,
            IEnumerable<IDiscoverableGeneratorComponentToBehaviorConverter> generatorComponentToBehaviorConverters,
            IEnumerable<IDiscoverablePredicateGeneratorComponentToBehaviorConverter> predicateGeneratorComponentToBehaviorConverters)
        {
            _mapping = new Dictionary<Type, ConvertGeneratorComponentToBehaviorDelegate>();
            _predicateMapping = new List<Tuple<Predicate<IGeneratorComponent>, ConvertGeneratorComponentToBehaviorDelegate>>();

            logger.Debug($"Registering converters to '{this}'...");

            foreach (var converter in generatorComponentToBehaviorConverters)
            {
                logger.Debug(
                    $"Registering '{converter}' on type '{converter.ComponentType}' " +
                    $"to '{this}'.");
                Register(
                    converter.ComponentType,
                    converter.Convert);
            }

            foreach (var converter in predicateGeneratorComponentToBehaviorConverters)
            {
                logger.Debug($"Registering predicate-based '{converter}' to '{this}'.");
                Register(
                    converter.CanConvert,
                    converter.Convert);
            }

            logger.Debug($"Done registering converters to '{this}'.");
        }

        public void Register<T>(ConvertGeneratorComponentToBehaviorDelegate callback)
        {
            Register(typeof(T), callback);
        }

        public void Register(
            Type t,
            ConvertGeneratorComponentToBehaviorDelegate callback)
        {
            _mapping.Add(t, callback);
        }

        public void Register(
            Predicate<IGeneratorComponent> predicate,
            ConvertGeneratorComponentToBehaviorDelegate callback)
        {
            _predicateMapping.Add(Tuple.Create(predicate, callback));
        }

        public IEnumerable<IBehavior> Convert(
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent) =>
            Convert(baseBehaviors, new[] { generatorComponent });

        public IEnumerable<IBehavior> Convert(
            IEnumerable<IBehavior> baseBehaviors,
            IEnumerable<IGeneratorComponent> generatorComponents)
        {
            var accumulatedBehaviors = new List<IBehavior>(baseBehaviors);
            foreach (var generatorComponent in generatorComponents)
            {
                if (!_mapping.TryGetValue(
                    generatorComponent.GetType(),
                    out var convertCallback))
                {
                    convertCallback = _predicateMapping
                        .FirstOrDefault(x => x.Item1(generatorComponent))
                        ?.Item2;
                    if (convertCallback == null)
                    {
                        throw new InvalidOperationException(
                            "There was no registered mapping to convert " +
                            $"'{generatorComponent.GetType()}'.");
                    }
                }

                var converted = convertCallback
                    .Invoke(
                        accumulatedBehaviors,
                        generatorComponent)
                    .ToArray();
                accumulatedBehaviors.AddRange(converted);
            }

            return accumulatedBehaviors;
        }
    }
}