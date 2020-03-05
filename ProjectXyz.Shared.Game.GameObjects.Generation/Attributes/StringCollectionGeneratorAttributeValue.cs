using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Attributes
{
    [DebuggerDisplay("{string.Join(\", \", Values)}")]
    public sealed class StringCollectionGeneratorAttributeValue : IGeneratorAttributeValue
    {
        public StringCollectionGeneratorAttributeValue(params string[] values)
            : this((IEnumerable<string>)values)
        {
        }

        public StringCollectionGeneratorAttributeValue(IEnumerable<string> values)
        {
            Values = values.ToArray();
        }

        public IReadOnlyCollection<string> Values { get; }
    }
}