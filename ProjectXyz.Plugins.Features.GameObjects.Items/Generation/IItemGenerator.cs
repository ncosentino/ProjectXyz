using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public interface IItemGenerator : IHasFilterAttributes
    {
        IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext);
    }
}