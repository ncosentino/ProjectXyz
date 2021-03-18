using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Autofac
{
    public sealed class ActorsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<MergeStatsActorBehaviorsInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoAdditionalActorBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorBehaviorsInterceptorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ActorBehaviorsProviderFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}