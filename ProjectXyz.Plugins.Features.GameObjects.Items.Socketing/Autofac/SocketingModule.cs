using Autofac;

using ProjectXyz.Framework.Autofac;

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
        }
    }
}