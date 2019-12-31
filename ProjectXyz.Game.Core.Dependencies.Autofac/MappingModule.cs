using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Game.Core.Mapping;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class MappingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}