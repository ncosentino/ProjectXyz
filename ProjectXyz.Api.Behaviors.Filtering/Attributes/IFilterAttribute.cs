using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Behaviors.Filtering.Attributes
{
    public interface IFilterAttribute
    {
        IIdentifier Id { get; }

        IFilterAttributeValue Value { get; }

        bool Required { get; }
    }
}