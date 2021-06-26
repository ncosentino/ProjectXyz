using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

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
                .IfNotRegistered(typeof(IDiscoverableActorBehaviorsProvider))
                .SingleInstance();
            builder
               .RegisterType<NoActorIdentifiers>()
               .AsImplementedInterfaces()
               .IfNotRegistered(typeof(IActorIdentifiers))
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