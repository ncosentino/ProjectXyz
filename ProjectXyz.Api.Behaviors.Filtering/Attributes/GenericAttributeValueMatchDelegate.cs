namespace ProjectXyz.Api.Behaviors.Filtering.Attributes
{
    public delegate bool GenericAttributeValueMatchDelegate<T1, T2>(
        T1 v1,
        T2 v2)
        where T1 : IFilterAttributeValue
        where T2 : IFilterAttributeValue;
}