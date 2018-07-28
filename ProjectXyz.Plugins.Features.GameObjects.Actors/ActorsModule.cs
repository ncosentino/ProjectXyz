using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ActorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoAdditionalActorBehaviorsProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}