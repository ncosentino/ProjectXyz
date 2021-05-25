using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.Filtering.Default.Attributes
{
    public sealed class NotFilterAttributeValue : IFilterAttributeValue
    {
        public NotFilterAttributeValue(IFilterAttributeValue wrapped)
        {
            Wrapped = wrapped;
        }

        public IFilterAttributeValue Wrapped { get; }

        public override string ToString() =>
            $"Not ({Wrapped})";
    }
}