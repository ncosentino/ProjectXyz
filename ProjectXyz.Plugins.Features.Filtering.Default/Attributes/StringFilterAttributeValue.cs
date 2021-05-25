using System;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
{
    public sealed class StringFilterAttributeValue : IFilterAttributeValue
    {
        private readonly Lazy<string> _lazyValue;

        public StringFilterAttributeValue(string value)
            : this(() => value)
        {
        }

        public StringFilterAttributeValue(Func<string> callback)
        {
            _lazyValue = new Lazy<string>(callback);
        }

        public string Value => _lazyValue.Value;

        public override string ToString() => Value;
    }
}