using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public interface IBaseItemGenerator
    {
        IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext);
    }
}