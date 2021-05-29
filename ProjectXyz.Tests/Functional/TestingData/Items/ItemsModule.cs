using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Tests.Functional.TestingData.Items
{
    public sealed class ItemsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<ItemIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class ItemIdentifiers : IItemIdentifiers
    {
        public IIdentifier ItemDefinitionIdentifier => new StringIdentifier("item-id");
    }
}
