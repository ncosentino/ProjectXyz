using Autofac;

namespace Examples.Modules.ExpressionEnchantments
{
    public sealed class ExpressionEnchantmentsModule : Module
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