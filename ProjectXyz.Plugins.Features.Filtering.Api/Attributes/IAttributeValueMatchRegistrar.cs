namespace ProjectXyz.Plugins.Features.Filtering.Api.Attributes
{
    public interface IAttributeValueMatchRegistrar
    {
        void Register<T1, T2>(GenericAttributeValueMatchDelegate<T1, T2> callback)
            where T1 : IFilterAttributeValue;

        void Register(
            AttributeValueMatchDelegate canMatchCallback,
            AttributeValueMatchDelegate callback);
    }
}