using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Default.Autofac
{
    public sealed class ItemsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ItemFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}