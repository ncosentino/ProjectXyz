using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Default.Autofac
{
    public sealed class CraftingModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<CraftingHandlerFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ReplaceIngredientsCraftingRepositoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ReplaceIngredientsCraftingHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}