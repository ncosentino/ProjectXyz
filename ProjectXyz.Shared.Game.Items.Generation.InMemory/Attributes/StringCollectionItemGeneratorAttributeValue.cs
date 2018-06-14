using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory.Attributes
{
    public sealed class StringCollectionItemGeneratorAttributeValue : IItemGeneratorAttributeValue
    {
        public StringCollectionItemGeneratorAttributeValue(params string[] values)
            : this((IEnumerable<string>)values)
        {
        }

        public StringCollectionItemGeneratorAttributeValue(IEnumerable<string> values)
        {
            Values = values.ToArray();
        }

        public IReadOnlyCollection<string> Values { get; }
    }
}