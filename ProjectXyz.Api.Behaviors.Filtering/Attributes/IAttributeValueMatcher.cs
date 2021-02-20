namespace ProjectXyz.Api.Behaviors.Filtering.Attributes
{
    public interface IAttributeValueMatcher
    {
        bool Match(
            IFilterAttributeValue v1,
            IFilterAttributeValue v2);
    }
}