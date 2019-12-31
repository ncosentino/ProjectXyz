using System.Collections.Generic;
using Autofac;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Autofac
{
    public sealed class ProvidedImplementationsModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            // TODO: should this be in the other project for shared "generation" classes?
            builder
                .RegisterType<BaseItemGenerator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            // TODO: should this be in the other project for shared "generation" classes?
            builder
                .RegisterType<ItemGeneratorFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    x
                     .Context
                     .Resolve<IEnumerable<IDiscoverableItemGenerator>>()
                     .Foreach(x.Instance.Register);
                });

            builder
                .RegisterType<ItemFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}