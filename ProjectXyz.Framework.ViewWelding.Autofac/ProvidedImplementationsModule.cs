using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Framework.ViewWelding.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ViewWelderFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
