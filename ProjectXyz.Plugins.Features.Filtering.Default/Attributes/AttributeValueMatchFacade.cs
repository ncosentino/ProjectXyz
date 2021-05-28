using System;
using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
{
    public sealed class AttributeValueMatchFacade : IAttributeValueMatchFacade
    {
        private readonly Dictionary<Tuple<Type, Type>, AttributeValueMatchDelegate> _mapping;
        private readonly List<Tuple<AttributeValueMatchDelegate, AttributeValueMatchDelegate>> _delegateMappingObjects;

        public AttributeValueMatchFacade()
        {
            _mapping = new Dictionary<Tuple<Type, Type>, AttributeValueMatchDelegate>();
            _delegateMappingObjects = new List<Tuple<AttributeValueMatchDelegate, AttributeValueMatchDelegate>>();
        }

        public void Register<T1, T2>(GenericAttributeValueMatchDelegate<T1, T2> callback)
            where T1 : IFilterAttributeValue
        {
            _mapping.Add(
                new Tuple<Type, Type>(typeof(T1), typeof(T2)),
                (v1, v2) => callback((T1)v1, (T2)v2));
        }

        public void Register(
            AttributeValueMatchDelegate canMatchCallback,
            AttributeValueMatchDelegate callback)
        {
            _delegateMappingObjects.Add(Tuple.Create(canMatchCallback, callback));
        }

        public bool Match(
            IFilterAttributeValue filterAttributeValue,
            object obj)
        {
            var objType = obj.GetType();
            var filterType = filterAttributeValue.GetType();
            var mappingKey = new Tuple<Type, Type>(objType, filterType);

            if (!_mapping.TryGetValue(
                mappingKey,
                out var matchCallback))
            {
                foreach (var entry in _mapping)
                {
                    if (entry.Key.Item1.IsAssignableFrom(filterType) &&
                        entry.Key.Item2.IsAssignableFrom(objType))
                    {
                        matchCallback = entry.Value;
                        _mapping[mappingKey] = entry.Value;
                        break;
                    }
                }
            }

            foreach (var entry in _delegateMappingObjects)
            {
                if (entry.Item1(filterAttributeValue, obj))
                {
                    matchCallback = entry.Item2;
                    break;
                }
            }

            if (matchCallback == null)
            {
                throw new InvalidOperationException(
                    $"There is no attribute matching callback for types " +
                    $"'{filterAttributeValue.GetType()}' and '{obj.GetType()}'.");
            }

            var isMatch = matchCallback(filterAttributeValue, obj);
            return isMatch;
        }

        public bool Match(
            IFilterAttributeValue v1,
            IFilterAttributeValue v2)
        {
            var v1Type = v1.GetType();
            var v2Type = v2.GetType();

            if (!_mapping.TryGetValue(
                new Tuple<Type, Type>(v1Type, v2Type),
                out var matchCallback))
            {
                if (_mapping.TryGetValue(
                    new Tuple<Type, Type>(v2Type, v1Type),
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
                        if (entry.Key.Item1.IsAssignableFrom(v1Type) &&
                            entry.Key.Item2.IsAssignableFrom(v2Type))
                        {
                            matchCallback = entry.Value;
                            _mapping[new Tuple<Type, Type>(v1Type, v2Type)] = entry.Value;
                            break;
                        }
                        else if (entry.Key.Item1.IsAssignableFrom(v2Type) &&
                            entry.Key.Item2.IsAssignableFrom(v1Type))
                        {
                            var temp = v2;
                            v2 = v1;
                            v1 = temp;

                            matchCallback = entry.Value;
                            _mapping[new Tuple<Type, Type>(v2Type, v1Type)] = entry.Value;
                            break;
                        }
                    }
                }

                foreach (var entry in _delegateMappingObjects)
                {
                    if (entry.Item1(v1, v2))
                    {
                        matchCallback = entry.Item2;
                        break;
                    }
                }

                if (matchCallback == null)
                {
                    throw new InvalidOperationException(
                        $"There is no attribute matching callback for types " +
                        $"'{v1Type}' and '{v2Type}'.");
                }
            }

            var isMatch = matchCallback(v1, v2);
            return isMatch;
        }
    }
}