using Autofac;
using ProjectXyz.Game.Core.Items;

namespace ProjectXyz.Game.Core.Dependencies.Autofac
{
    public sealed class ItemsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder
                .RegisterType<ItemGenerationContextFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ItemGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}