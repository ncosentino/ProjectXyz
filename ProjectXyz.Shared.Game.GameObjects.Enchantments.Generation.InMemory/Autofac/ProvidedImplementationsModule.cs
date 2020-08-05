using Autofac;
using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            // FIXME: why is in-memory even SHARED?!
            builder
                .RegisterType<EnchantmentGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}