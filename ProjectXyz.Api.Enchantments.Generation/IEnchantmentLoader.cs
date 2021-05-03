using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
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