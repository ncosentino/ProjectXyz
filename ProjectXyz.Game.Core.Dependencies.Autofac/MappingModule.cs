using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Game.Core.Mapping;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class MappingModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<MapManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}