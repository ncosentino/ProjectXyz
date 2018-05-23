using Autofac;
using ProjectXyz.Game.Core.Behaviors;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class BehaviorModule : Module
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