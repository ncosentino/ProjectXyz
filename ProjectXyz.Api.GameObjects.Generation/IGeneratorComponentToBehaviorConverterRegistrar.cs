using System;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public interface IGeneratorComponentToBehaviorConverterRegistrar
    {
        void Register<T>(ConvertGeneratorComponentToBehaviorDelegate callback);

        void Register(
            Type t,
            ConvertGeneratorComponentToBehaviorDelegate callback);
    }
}