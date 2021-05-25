using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
{
    public sealed class AllIdentifierCollectionFilterAttributeValue : IFilterAttributeValue
    {
        private readonly Lazy<IReadOnlyCollection<IIdentifier>> _lazyValues;

        public AllIdentifierCollectionFilterAttributeValue(params IIdentifier[] values)
            : this((IEnumerable<IIdentifier>)values)
        {
        }

        public AllIdentifierCollectionFilterAttributeValue(IEnumerable<IIdentifier> values)
            : this(() => values.ToArray())
        {
        }

        public AllIdentifierCollectionFilterAttributeValue(Func<IReadOnlyCollection<IIdentifier>> callback)
        {
            _lazyValues = new Lazy<IReadOnlyCollection<IIdentifier>>(callback);
        }

        public IReadOnlyCollection<IIdentifier> Values => _lazyValues.Value;

        public override string ToString() =>
            $"All of {string.Join(", ", Values.Select(x => x.ToString()))}";
    }
}