using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<BaseItemGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                 .RegisterType<LootGenerator>()
                 .AsImplementedInterfaces()
                 .SingleInstance();
            builder
                .RegisterType<ItemDropTableHandlerGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LinkedDropTableHandlerGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
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