using System;

namespace ProjectXyz.Api.Behaviors.Filtering
{
    public interface IFilterComponentToBehaviorConverterRegistrar
    {
        void Register<T>(ConvertFilterComponentToBehaviorDelegate callback);

        void Register(
            Type t,
            ConvertFilterComponentToBehaviorDelegate callback);

        void Register(
            Predicate<IFilterComponent> predicate,
            ConvertFilterComponentToBehaviorDelegate callback);
    }
}