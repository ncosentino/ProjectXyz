using System;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
{
    public sealed class BooleanFilterAttributeValue : IFilterAttributeValue
    {
        private readonly Lazy<bool> _lazyValue;

        public BooleanFilterAttributeValue(bool value)
            : this(() => value)
        {
        }

        public BooleanFilterAttributeValue(Func<bool> callback)
        {
            _lazyValue = new Lazy<bool>(callback);
        }

        public bool Value => _lazyValue.Value;

        public override string ToString() =>
            $"Bool: {Value}";
    }
}