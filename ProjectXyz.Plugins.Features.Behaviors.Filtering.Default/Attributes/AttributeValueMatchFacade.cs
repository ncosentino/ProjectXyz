using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes
{
    public sealed class AttributeValueMatchFacade : IAttributeValueMatchFacade
    {
        private readonly Dictionary<Tuple<Type, Type>, AttributeValueMatchDelegate> _mapping;

        public AttributeValueMatchFacade()
        {
            _mapping = new Dictionary<Tuple<Type, Type>, AttributeValueMatchDelegate>();
        }

        public void Register<T1, T2>(GenericAttributeValueMatchDelegate<T1, T2> callback)
            where T1 : IFilterAttributeValue
            where T2 : IFilterAttributeValue
        {
            _mapping.Add(
                new Tuple<Type, Type>(typeof(T1), typeof(T2)),
                (v1, v2) => callback((T1)v1, (T2)v2));
        }

        public bool Match(
            IFilterAttributeValue v1,
            IFilterAttributeValue v2)
        {
            if (!_mapping.TryGetValue(
                new Tuple<Type, Type>(v1.GetType(), v2.GetType()),
                out var matchCallback))
            {
                if (_mapping.TryGetValue(
                    new Tuple<Type, Type>(v2.GetType(), v1.GetType()),
                    out matchCallback))
                {
                    var temp = v2;
                    v2 = v1;
                    v1 = temp;
                }
                else
                {
                    foreach (var entry in _mapping)
                    {
                        if (entry.Key.Item1.IsAssignableFrom(v1.GetType()) &&
                            entry.Key.Item2.IsAssignableFrom(v2.GetType()))
                        {
                            matchCallback = entry.Value;
                            break;
                        }
                        else if (entry.Key.Item1.IsAssignableFrom(v2.GetType()) &&
                            entry.Key.Item2.IsAssignableFrom(v1.GetType()))
                        {
                            var temp = v2;
                            v2 = v1;
                            v1 = temp;

                            matchCallback = entry.Value;
                            break;
                        }
                    }
                }

                if (matchCallback == null)
                {
                    throw new InvalidOperationException(
                        $"There is no attribute matching callback for types " +
                        $"'{v1.GetType()}' and '{v2.GetType()}'.");
                }
            }

            var isMatch = matchCallback(v1, v2);
            return isMatch;
        }
    }
}