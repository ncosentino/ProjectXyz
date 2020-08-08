using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Autofac
{
    public sealed class InternalDependenciesModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            ////builder
            ////    .RegisterType<StringSerializableDtoDataConverter>()
            ////    .AsImplementedInterfaces()
            ////    .SingleInstance();
            ////builder
            ////    .RegisterType<StringSerializableConverter>()
            ////    .AsImplementedInterfaces()
            ////    .SingleInstance();
        }
    }
}
