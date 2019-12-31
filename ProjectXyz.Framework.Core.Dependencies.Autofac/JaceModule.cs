using System;
using Autofac;
using Jace;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Shared.Framework.Math;

namespace ProjectXyz.Plugins.Framework.Math.Jace
{
    public sealed class JaceModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
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