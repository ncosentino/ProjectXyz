using Autofac;

namespace ConsoleApplication1.Modules
{
    public sealed class WipDependencies : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<StatPrinterSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
