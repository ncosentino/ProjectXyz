using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IAffixMagicTypes
    {
        #region Properties
        Guid Id { get; }

        IEnumerable<Guid> MagicTypeIds { get; }
        #endregion
    }
}
