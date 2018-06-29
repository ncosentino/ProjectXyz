using System.Collections.Generic;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables
{
    public interface IDropTableRepository
    {
        IEnumerable<IDropTable> GetAllDropTables();

        IDropTable GetForDropTableId(IIdentifier dropTableId);
    }
}