using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Behaviors.Autofac
{
    public sealed class BehaviorModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<BehaviorCollectionFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BehaviorFinder>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<BehaviorManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}