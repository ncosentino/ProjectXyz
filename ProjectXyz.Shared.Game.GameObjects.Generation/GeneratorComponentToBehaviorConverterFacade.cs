using System;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Shared.Game.GameObjects.Generation
{
    public sealed class GeneratorComponentToBehaviorConverterFacade : IGeneratorComponentToBehaviorConverterFacade
    {
        private readonly Dictionary<Type, ConvertGeneratorComponentToBehaviorDelegate> _mapping;

        public GeneratorComponentToBehaviorConverterFacade()
        {
            _mapping = new Dictionary<Type, ConvertGeneratorComponentToBehaviorDelegate>();
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

        public IEnumerable<IBehavior> Convert(IGeneratorComponent generatorComponent)
        {
            ConvertGeneratorComponentToBehaviorDelegate convertCallback;
            if (!_mapping.TryGetValue(
                generatorComponent.GetType(),
                out convertCallback))
            {
                throw new InvalidOperationException(
                    "There was no registered mapping to convert " +
                    $"'{generatorComponent.GetType()}'.");
            }

            var converted = convertCallback.Invoke(generatorComponent);
            return converted;
        }
    }
}