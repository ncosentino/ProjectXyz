using Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Item;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables.Implementations.Linked;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Autofac.DropTables.Implementations
{
    //
    // TODO: split this up and into different projects
    //
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder
                .RegisterType<ItemDropTableHandlerGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<LinkedDropTableHandlerGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}