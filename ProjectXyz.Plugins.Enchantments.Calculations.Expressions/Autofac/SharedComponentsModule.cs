using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments.Autofac
{
    public sealed class SharedComponentsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ContextToExpressionInterceptorConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
