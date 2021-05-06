using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Serialization.Newtonsoft;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Autofac
{
    public sealed class SocketingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ApplySocketEnchantmentsBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CanBeSocketedBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CanFitSocketBehaviorFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CanBeSocketedGameObjectsForBehavior>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<CanBeSocketedBehaviorSerializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}