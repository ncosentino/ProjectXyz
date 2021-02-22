using System;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes
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