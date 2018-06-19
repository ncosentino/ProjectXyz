namespace ProjectXyz.Api.GameObjects.Generation.Attributes
{
    public interface IAttributeValueMatchRegistrar
    {
        void Register<T1, T2>(GenericAttributeValueMatchDelegate<T1, T2> callback)
            where T1 : IGeneratorAttributeValue
            where T2 : IGeneratorAttributeValue;
    }
}