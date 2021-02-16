using System.Collections.Generic;
using System.Linq;

using Autofac;

using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ItemGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}