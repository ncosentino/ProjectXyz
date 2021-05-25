using System;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
{
    public sealed class DoubleFilterAttributeValue : IFilterAttributeValue
    {
        private readonly Lazy<double> _lazyValue;

        public DoubleFilterAttributeValue(double value)
            : this(() => value)
        {
        }

        public DoubleFilterAttributeValue(Func<double> callback)
        {
            _lazyValue = new Lazy<double>(callback);
        }

        public double Value => _lazyValue.Value;

        public override string ToString() =>
            $"{Value}d";
    }
}