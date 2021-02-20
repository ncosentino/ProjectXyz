using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Shared.Behaviors.Filtering
{
    public sealed class FilterComponentToBehaviorConverterFacade : IFilterComponentToBehaviorConverterFacade
    {
        private readonly Dictionary<Type, ConvertFilterComponentToBehaviorDelegate> _mapping;

        public FilterComponentToBehaviorConverterFacade()
        {
            _mapping = new Dictionary<Type, ConvertFilterComponentToBehaviorDelegate>();
        }

        public void Register<T>(ConvertFilterComponentToBehaviorDelegate callback)
        {
            Register(typeof(T), callback);
        }

        public void Register(
            Type t,
            ConvertFilterComponentToBehaviorDelegate callback)
        {
            _mapping.Add(t, callback);
        }

        public IEnumerable<IBehavior> Convert(IFilterComponent filterComponent)
        {
            if (!_mapping.TryGetValue(
                filterComponent.GetType(),
                out var convertCallback))
            {
                throw new InvalidOperationException(
                    "There was no registered mapping to convert " +
                    $"'{filterComponent.GetType()}'.");
            }

            var converted = convertCallback.Invoke(filterComponent);
            return converted;
        }
    }
}