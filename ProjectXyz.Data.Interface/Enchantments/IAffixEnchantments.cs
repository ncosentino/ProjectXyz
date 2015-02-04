using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IAffixEnchantments
    {
        #region Properties
        Guid Id { get; }

        Guid AffixId { get; }

        IEnumerable<Guid> EnchantmentIds { get; }
        #endregion
    }
}
