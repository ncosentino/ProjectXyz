using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IAffixMagicTypesRepository
    {
        #region Methods
        IAffixMagicTypes GetById(Guid id);
        #endregion
    }
}
