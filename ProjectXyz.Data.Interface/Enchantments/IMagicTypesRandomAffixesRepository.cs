using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IMagicTypesRandomAffixesRepositoryContract))]
    public interface IMagicTypesRandomAffixesRepository
    {
        #region Methods
        IMagicTypesRandomAffixes GetForMagicTypeId(Guid id);
        #endregion
    }
}
