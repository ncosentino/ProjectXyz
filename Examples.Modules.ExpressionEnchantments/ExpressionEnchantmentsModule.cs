using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.ExpressionEnchantments
{
    public sealed class ExpressionEnchantmentsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ValueMapperRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}