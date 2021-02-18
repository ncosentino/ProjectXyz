using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    public sealed class AllStringCollectionGeneratorAttributeValue : IGeneratorAttributeValue
    {
        public AllStringCollectionGeneratorAttributeValue(params string[] values)
            : this((IEnumerable<string>)values)
        {
        }

        public AllStringCollectionGeneratorAttributeValue(IEnumerable<string> values)
        {
            Values = values.ToArray();
        }

        public IReadOnlyCollection<string> Values { get; }

        public override string ToString() =>
            $"All of {string.Join(", ", Values)}";
    }
}