namespace ProjectXyz.Api.Behaviors.Filtering.Attributes
{
    public delegate bool AttributeValueMatchDelegate(
        IFilterAttributeValue v1,
        IFilterAttributeValue v2);
}