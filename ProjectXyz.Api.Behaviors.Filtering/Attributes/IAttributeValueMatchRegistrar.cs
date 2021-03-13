namespace ProjectXyz.Api.Behaviors.Filtering.Attributes
{
    public interface IAttributeValueMatchRegistrar
    {
        void Register<T1, T2>(GenericAttributeValueMatchDelegate<T1, T2> callback)
            where T1 : IFilterAttributeValue
            where T2 : IFilterAttributeValue;

        void Register(
            AttributeValueMatchDelegate canMatchCallback,
            AttributeValueMatchDelegate callback);
    }
}