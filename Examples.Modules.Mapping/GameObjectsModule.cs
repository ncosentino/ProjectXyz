using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.Mapping
{
    public sealed class GameObjectsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<MapRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}