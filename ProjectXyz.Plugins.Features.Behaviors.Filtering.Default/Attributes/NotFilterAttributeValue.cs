using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes
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