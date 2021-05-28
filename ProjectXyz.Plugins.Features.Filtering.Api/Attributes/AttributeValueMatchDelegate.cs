namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public delegate bool AttributeValueMatchDelegate(
        IFilterAttributeValue filterAttributeValue,
        object obj);
}