namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public interface IAttributeValueMatcher
    {
        bool Match(
            IFilterAttributeValue v1,
            IFilterAttributeValue v2);

        bool Match(
            IFilterAttributeValue filterAttributeValue,
            object obj);
    }
}