using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Game.Tests.Functional.TestingData.Enchantments
{
    public sealed class EnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c => new ValueMapperRepository(c.Resolve<TestData>()))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}