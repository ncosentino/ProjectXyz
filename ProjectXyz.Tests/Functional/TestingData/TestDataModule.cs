using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Tests.Functional.TestingData
{
    public sealed class TestDataModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TestData>()
                ////.AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}