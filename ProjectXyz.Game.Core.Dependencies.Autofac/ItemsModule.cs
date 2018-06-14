using Autofac;
using ProjectXyz.Game.Core.GameObjects.Items;
using ProjectXyz.Game.Core.GameObjects.Items.Generation;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class ItemsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder
                .RegisterType<ItemGeneratorContextFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}