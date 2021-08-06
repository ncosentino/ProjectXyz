using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Default.Autofac
{
    public sealed class ElapsedTimeModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<RealTimeProvider>()
                .AsImplementedInterfaces()
                .IfNotRegistered(typeof(IRealTimeProvider))
                .SingleInstance();
            builder
                .RegisterType<ElapsedTimeComponentCreator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
