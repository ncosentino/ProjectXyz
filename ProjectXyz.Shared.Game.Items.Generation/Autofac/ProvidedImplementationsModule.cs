using System.Collections.Generic;
using System.Linq;

using Autofac;

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
                .RegisterType<ItemDropTableFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LinkedDropTableFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<DropTableRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneDropTableRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemDefinitionRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneItemDefinitionRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
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
                    // NOTE: currently this needs to be OnActivated and not
                    // done with autofac discovery because of a circular
                    // dependency w/ classes that want IDropTableHandlerGeneratorFacade
                    x
                     .Context
                     .Resolve<IEnumerable<IDiscoverableDropTableHandlerGenerator>>()
                     .Foreach(d => x.Instance.Register(d.DropTableType, d));
                });
        }
    }
}