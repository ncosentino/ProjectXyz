using Autofac;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Autofac.DropTables
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder
                .RegisterType<LootGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}