using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.DropTables
{
    public sealed class NoneDropTableRepository : IDiscoverableDropTableRepository
    {
        public IEnumerable<IDropTable> GetAllDropTables() =>
            Enumerable.Empty<IDropTable>();

        public IDropTable GetForDropTableId(IIdentifier dropTableId) => null;
    }
}