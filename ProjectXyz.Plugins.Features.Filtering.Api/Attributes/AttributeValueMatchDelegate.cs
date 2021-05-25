namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public delegate bool AttributeValueMatchDelegate(
        IFilterAttributeValue v1,
        IFilterAttributeValue v2);
}