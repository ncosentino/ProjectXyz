
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Tests.Functional.TestingData
{
    public sealed class DropTableIdentifiers : IDropTableIdentifiers
    {
        public IIdentifier FilterContextDropTableIdentifier { get; } = new StringIdentifier("drop-table");
    }
}
