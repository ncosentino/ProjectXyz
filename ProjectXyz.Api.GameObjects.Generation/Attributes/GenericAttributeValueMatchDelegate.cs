namespace ProjectXyz.Api.GameObjects.Generation.Attributes
{
    public delegate bool GenericAttributeValueMatchDelegate<T1, T2>(
        T1 v1,
        T2 v2)
        where T1 : IGeneratorAttributeValue
        where T2 : IGeneratorAttributeValue;
}