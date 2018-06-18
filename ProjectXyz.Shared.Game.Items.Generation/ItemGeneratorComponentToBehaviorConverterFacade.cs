using System;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Items.Generation;

namespace ProjectXyz.Shared.Game.Items.Generation
{
    public sealed class ItemGeneratorComponentToBehaviorConverterFacade : IItemGeneratorComponentToBehaviorConverterFacade
    {
        private readonly Dictionary<Type, ConvertItemGeneratorComponentToBehaviorDelegate> _mapping;

        public ItemGeneratorComponentToBehaviorConverterFacade()
        {
            _mapping = new Dictionary<Type, ConvertItemGeneratorComponentToBehaviorDelegate>();
        }

        public void Register<T>(ConvertItemGeneratorComponentToBehaviorDelegate callback)
        {
            _mapping.Add(typeof(T), callback);
        }

        public IEnumerable<IBehavior> Convert(IItemGeneratorComponent itemGeneratorComponent)
        {
            ConvertItemGeneratorComponentToBehaviorDelegate convertCallback;
            if (!_mapping.TryGetValue(
                itemGeneratorComponent.GetType(),
                out convertCallback))
            {
                throw new InvalidOperationException(
                    "There was no registered mapping to convert " +
                    $"'{itemGeneratorComponent.GetType()}'.");
            }

            var converted = convertCallback.Invoke(itemGeneratorComponent);
            return converted;
        }
    }
}