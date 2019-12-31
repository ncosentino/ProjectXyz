using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.ExpressionEnchantments.Api;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments.Autofac
{
    public sealed class DepencencyModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
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
