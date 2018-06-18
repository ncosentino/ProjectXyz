namespace ProjectXyz.Api.Items.Generation
{
    public interface IItemGeneratorComponentToBehaviorConverterRegistrar
    {
        void Register<T>(ConvertItemGeneratorComponentToBehaviorDelegate callback);
    }
}