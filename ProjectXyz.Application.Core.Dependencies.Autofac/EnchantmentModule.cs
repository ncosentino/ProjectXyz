using System.Collections.Generic;
using System.Linq;
using Autofac;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Framework.Entities.Interface;

namespace ProjectXyz.Application.Core.Dependencies.Autofac
{
    public sealed class EnchantmentModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c =>
                {
                    var contextToInterceptorsConverter = new ContextToInterceptorsConverter();
                    var contextToExpressionInterceptorConverters = c.Resolve<IEnumerable<IContextToExpressionInterceptorConverter>>();
                    foreach (var contextToExpressionInterceptorConverter in contextToExpressionInterceptorConverters)
                    {
                        contextToInterceptorsConverter.Register(contextToExpressionInterceptorConverter);
                    }

                    return contextToInterceptorsConverter;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantmentCalculator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<EnchantmentApplier>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => new EnchantmentCalculatorContextFactory(Enumerable.Empty<IComponent>()))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}