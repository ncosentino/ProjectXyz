using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Mapping.Default.PathFinding;

namespace ProjectXyz.Plugins.Features.Mapping.Default.Autofac
{
    public sealed class MappingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapGameObjectManagerSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapGameObjectManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapBehaviorsInterceptorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapBehaviorsProviderFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AStarPathFinder>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AStarPathFinderFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}