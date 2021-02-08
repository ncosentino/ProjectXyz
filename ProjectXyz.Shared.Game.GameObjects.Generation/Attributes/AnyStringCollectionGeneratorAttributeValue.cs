using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    [DebuggerDisplay("Any of {string.Join(\", \", Values)}")]
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
    }
}