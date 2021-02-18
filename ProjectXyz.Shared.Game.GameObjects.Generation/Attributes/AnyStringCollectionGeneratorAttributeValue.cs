﻿using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class AnyStringCollectionGeneratorAttributeValue : IGeneratorAttributeValue
    {
        public AnyStringCollectionGeneratorAttributeValue(params string[] values)
            : this((IEnumerable<string>)values)
        {
        }

        public AnyStringCollectionGeneratorAttributeValue(IEnumerable<string> values)
        {
            Values = values.ToArray();
        }

        public IReadOnlyCollection<string> Values { get; }

        public override string ToString() =>
            $"Any of {string.Join(", ", Values)}";
    }
}