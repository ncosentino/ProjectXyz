using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public interface IFilterAttribute
    {
        IIdentifier Id { get; }

        IFilterAttributeValue Value { get; }

        bool Required { get; }
    }
}