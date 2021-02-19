using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Behaviors.Autofac
{
    public sealed class BehaviorModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
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