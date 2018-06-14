using System;
using Autofac;
using ConsoleApplication1.Wip;
using ProjectXyz.Shared.Framework;

namespace ConsoleApplication1.Modules
{
    public sealed class WipDependencies : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new RandomNumberGenerator(new Random()))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<StatPrinterSystem>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
