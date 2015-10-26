using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items.Affixes
{
    public interface IItemAffix
    {
        #region Properties
        Guid ItemAffixDefinitionId { get; }

        Guid NameStringResourceId { get; }

        bool Prefix { get; }

        IEnumerable<IEnchantment> Enchantments { get; }
        #endregion
    }
}
