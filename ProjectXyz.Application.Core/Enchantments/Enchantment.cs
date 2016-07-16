using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class Enchantment : IEnchantment
    {
        public Enchantment(
            IIdentifier statDefinitionId,
            IEnumerable<IComponent> components)
        {
            StatDefinitionId = statDefinitionId;
            Components = new ComponentCollection(components);
        }

        public IIdentifier StatDefinitionId { get; }

        public IComponentCollection Components { get; }

        public override string ToString()
        {
            return $"Expression Enchantment\r\n\tStat Definition Id={StatDefinitionId}";
        }
    }
}