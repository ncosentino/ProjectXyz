using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation
{
    public interface IBaseItemGenerator
    {
        IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext);
    }
}