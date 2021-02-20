using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Api.Enchantments.Generation
{
    public interface IEnchantmentDefinition : IHasFilterAttributes
    {
        IEnumerable<IFilterComponent> FilterComponents { get; }
    }
}