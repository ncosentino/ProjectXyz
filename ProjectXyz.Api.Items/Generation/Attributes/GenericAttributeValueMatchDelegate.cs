namespace ProjectXyz.Api.Items.Generation.Attributes
{
    public delegate bool GenericAttributeValueMatchDelegate<T1, T2>(
        T1 v1,
        T2 v2)
        where T1 : IItemGeneratorAttributeValue
        where T2 : IItemGeneratorAttributeValue;
}