using Autofac;
using ProjectXyz.Shared.Game.GameObjects.Items.Generation;

namespace ProjectXyz.Shared.Game.GameObjects.Items.Autofac
{
    public sealed class ItemsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            // TODO: should this be in the other project for shared "generation" classes?
            builder
                .RegisterType<GeneratorContextFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            // TODO: should this be in the other project for shared "generation" classes?
            builder
                .RegisterType<BaseItemGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}