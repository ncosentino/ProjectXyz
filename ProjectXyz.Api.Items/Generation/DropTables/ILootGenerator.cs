using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation.DropTables
{
    public interface ILootGenerator
    {
        IEnumerable<IGameObject> GenerateLoot(IFilterContext filterContext);
    }
}