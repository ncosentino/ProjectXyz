using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IMagicTypesRandomAffixesRepository))]
    public abstract class IMagicTypesRandomAffixesRepositoryContract : IMagicTypesRandomAffixesRepository
    {
        #region Methods
        public abstract IMagicTypesRandomAffixes GetForMagicTypeId(Guid id);
        #endregion
    }
}
