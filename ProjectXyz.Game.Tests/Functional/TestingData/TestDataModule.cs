using Autofac;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class TestDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<TestData>()
                ////.AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}