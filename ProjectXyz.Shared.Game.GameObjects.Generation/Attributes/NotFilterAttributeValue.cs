using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Shared.Behaviors.Filtering.Attributes
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