using Autofac;
using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions.Autofac
{
    public sealed class DepencencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c =>
                {
                    var valueMappers = c.Resolve<IValueMapperRepository>()
                        .GetValueMappers();
                    var contextToTermValueMappingConverter = new ContextToTermValueMappingConverter(valueMappers);
                    return contextToTermValueMappingConverter;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => new ValueMappingExpressionInterceptorFactory(2))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
