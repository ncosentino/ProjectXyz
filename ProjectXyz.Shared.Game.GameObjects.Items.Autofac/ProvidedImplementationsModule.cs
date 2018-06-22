using Autofac;
using ProjectXyz.Shared.Game.GameObjects.Items.Generation;
using ProjectXyz.Shared.Game.GameObjects.Items.Generation.InMemory;

namespace ProjectXyz.Shared.Game.GameObjects.Items.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
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
            // TODO: should this be in the other project for shared "generation" classes?
            builder
                .RegisterType<ItemGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<ItemFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}