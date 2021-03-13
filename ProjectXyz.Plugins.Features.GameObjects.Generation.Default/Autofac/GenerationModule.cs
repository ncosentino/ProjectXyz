using Autofac;

using ProjectXyz.Framework.Autofac;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.Default.Autofac
{
    public sealed class GenerationModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
               .RegisterType<BehaviorGeneratorComponentToBehaviorConverter>()
               .AsImplementedInterfaces()
               .SingleInstance();
            builder
                .RegisterType<GeneratorComponentToBehaviorConverterFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
