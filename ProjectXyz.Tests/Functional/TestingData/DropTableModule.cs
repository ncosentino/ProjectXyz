using Autofac;

namespace ProjectXyz.Tests.Functional.TestingData
{
    public sealed class DropTableModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<DropTableIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
