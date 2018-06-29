using System.Collections.Generic;
using Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Api.Framework.Collections;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Autofac.DropTables
{
    public sealed class InternalDependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder
                .RegisterType<DropTableHandlerGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    x
                     .Context
                     .Resolve<IEnumerable<IDiscoverableDropTableHandlerGenerator>>()
                     .Foreach(d => x.Instance.Register(d.DropTableType, d));
                });
        }
    }
}