using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes
{
    public sealed class FilterAttribute : IFilterAttribute
    {
        public FilterAttribute(
            IIdentifier id,
            IFilterAttributeValue value,
            bool required)
        {
            Id = id;
            Value = value;
            Required = required;
        }

        public IIdentifier Id { get; }

        public IFilterAttributeValue Value { get; }

        public bool Required { get; }

        public override string ToString() =>
            $"{Id} - {Value}{(Required ? " Required" : string.Empty)}";
    }
}