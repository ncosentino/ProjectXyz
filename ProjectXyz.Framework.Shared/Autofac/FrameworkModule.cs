using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Framework.Autofac
{
    public sealed class FrameworkModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<IdentifierConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}