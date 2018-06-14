namespace ProjectXyz.Api.Items.Generation.Attributes
{
    public interface IAttributeValueMatchRegistrar
    {
        void Register<T1, T2>(GenericAttributeValueMatchDelegate<T1, T2> callback)
            where T1 : IItemGeneratorAttributeValue
            where T2 : IItemGeneratorAttributeValue;
    }
}