using System;
using Autofac;
using Jace;
using ProjectXyz.Shared.Framework.Math;

namespace ProjectXyz.Framework.Core.Dependencies.Autofac
{
    public sealed class FrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<CalculationEngine>()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var jaceCalculationEngine = c.Resolve<CalculationEngine>();
                    Func<string, double> calculateCallback = jaceCalculationEngine.Calculate;
                    return new GenericExpressionEvaluator(calculateCallback);
                })
                .SingleInstance()
                .AsImplementedInterfaces();
        }
    }
}