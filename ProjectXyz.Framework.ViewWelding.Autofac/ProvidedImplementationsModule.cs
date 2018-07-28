using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Framework.ViewWelding.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ViewWelderFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
