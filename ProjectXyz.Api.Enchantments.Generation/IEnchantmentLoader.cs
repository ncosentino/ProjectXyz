using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentLoader
    {
        IEnumerable<IGameObject> Load(IFilterContext filterContext);

        IEnumerable<IGameObject> LoadForEnchantmenDefinitionIds(IEnumerable<IIdentifier> enchantmentDefinitionIds);
    }
}