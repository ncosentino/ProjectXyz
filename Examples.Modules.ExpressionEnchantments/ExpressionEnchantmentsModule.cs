using Autofac;
using ProjectXyz.Framework.Autofac;

namespace Examples.Modules.ExpressionEnchantments
{
    public sealed class ExpressionEnchantmentsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ValueMapperRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}