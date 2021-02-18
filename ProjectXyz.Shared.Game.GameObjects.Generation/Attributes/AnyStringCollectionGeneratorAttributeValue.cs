using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class AnyStringCollectionGeneratorAttributeValue : IGeneratorAttributeValue
    {
        private readonly Lazy<IReadOnlyCollection<string>> _lazyValues;

        public AnyStringCollectionGeneratorAttributeValue(params string[] values)
            : this((IEnumerable<string>)values)
        {
        }

        public AnyStringCollectionGeneratorAttributeValue(IEnumerable<string> values)
            : this(() => values.ToArray())
        {
        }

        public AnyStringCollectionGeneratorAttributeValue(Func<IReadOnlyCollection<string>> callback)
        {
            _lazyValues = new Lazy<IReadOnlyCollection<string>>(callback);
        }

        public IReadOnlyCollection<string> Values => _lazyValues.Value;

        public override string ToString() =>
            $"Any of {string.Join(", ", Values)}";
    }
}