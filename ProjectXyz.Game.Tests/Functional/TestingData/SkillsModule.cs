using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Game.Tests.Functional.TestingData
{
    public sealed class DropTableModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .RegisterType<DropTableIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }

    public sealed class DropTableIdentifiers : IDropTableIdentifiers
    {
        public IIdentifier FilterContextDropTableIdentifier { get; } = new StringIdentifier("drop-table");
    }
}
