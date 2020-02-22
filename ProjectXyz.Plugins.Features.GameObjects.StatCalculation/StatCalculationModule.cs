using System;
using System.Collections.Generic;
using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Autofac
{
    public sealed class StatCalculationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<TargetNavigator>()
                .As<ITargetNavigator>()
                .SingleInstance();
            builder
                .RegisterType<StatCalculationService>()
                .As<IStatCalculationService>()
                .SingleInstance();
            builder.RegisterBuildCallback(c =>
            {
                var service = c.Resolve<IStatCalculationService>();
                foreach (var handler in c.Resolve<IEnumerable<IStatCalculatorHandler>>())
                {
                    service.Register(handler);
                }

                foreach (var handler in c.Resolve<IEnumerable<IGetEnchantmentsHandler>>())
                {
                    service.Register(handler);
                }
            });
        }
    }
}
