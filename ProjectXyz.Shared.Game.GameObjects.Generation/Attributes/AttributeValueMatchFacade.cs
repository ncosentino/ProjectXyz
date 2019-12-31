﻿using System;
using System.Collections.Generic;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class AttributeValueMatchFacade : IAttributeValueMatchFacade
    {
        private readonly Dictionary<Tuple<Type, Type>, AttributeValueMatchDelegate> _mapping;

        public AttributeValueMatchFacade()
        {
            _mapping = new Dictionary<Tuple<Type, Type>, AttributeValueMatchDelegate>();
        }

        public void Register<T1, T2>(GenericAttributeValueMatchDelegate<T1, T2> callback)
            where T1 : IGeneratorAttributeValue
            where T2 : IGeneratorAttributeValue
        {
            _mapping.Add(
                new Tuple<Type, Type>(typeof(T1), typeof(T2)),
                (v1, v2) => callback((T1)v1, (T2)v2));
        }

        public bool Match(
            IGeneratorAttributeValue v1,
            IGeneratorAttributeValue v2)
        {
            if (!_mapping.TryGetValue(
                new Tuple<Type, Type>(v1.GetType(), v2.GetType()),
                out var matchCallback))
            {
                throw new InvalidOperationException(
                    $"There is no attribute matching callback for types '{v1.GetType()}' and '{v2.GetType()}'.");
            }

            var isMatch = matchCallback(v1, v2);
            return isMatch;
        }
    }
}