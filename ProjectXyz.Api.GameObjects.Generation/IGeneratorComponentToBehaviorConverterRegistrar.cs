namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IGeneratorComponentToBehaviorConverterRegistrar
    {
        void Register<T>(ConvertGeneratorComponentToBehaviorDelegate callback);
    }
}