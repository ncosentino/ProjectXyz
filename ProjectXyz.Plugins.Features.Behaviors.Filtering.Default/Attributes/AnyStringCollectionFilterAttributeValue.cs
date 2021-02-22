using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes
{
    public sealed class AnyStringCollectionFilterAttributeValue : IFilterAttributeValue
    {
        private readonly Lazy<IReadOnlyCollection<string>> _lazyValues;

        public AnyStringCollectionFilterAttributeValue(params string[] values)
            : this((IEnumerable<string>)values)
        {
        }

        public AnyStringCollectionFilterAttributeValue(IEnumerable<string> values)
            : this(() => values.ToArray())
        {
        }

        public AnyStringCollectionFilterAttributeValue(Func<IReadOnlyCollection<string>> callback)
        {
            _lazyValues = new Lazy<IReadOnlyCollection<string>>(callback);
        }

        public IReadOnlyCollection<string> Values => _lazyValues.Value;

        public override string ToString() =>
            $"Any of {string.Join(", ", Values)}";
    }
}