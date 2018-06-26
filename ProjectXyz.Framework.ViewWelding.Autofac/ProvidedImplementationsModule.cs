using Autofac;

namespace ProjectXyz.Framework.ViewWelding.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
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
