using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
using ProjectXyz.Application.Enchantments.Core.Calculations;
using ProjectXyz.Shared.Framework.Entities;

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
                .Register(c =>
                {
                    var stateContextProvider = c.Resolve<IStateContextProvider>();
                    var stateContextProviderComponent = new GenericComponent<IStateContextProvider>(stateContextProvider);
                    return new EnchantmentCalculatorContextFactory(new[]
                    {
                        stateContextProviderComponent
                    });
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}