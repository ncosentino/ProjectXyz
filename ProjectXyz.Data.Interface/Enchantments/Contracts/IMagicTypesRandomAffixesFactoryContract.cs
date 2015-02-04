using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IMagicTypesRandomAffixesFactory))]
    public abstract class IMagicTypesRandomAffixesFactoryContract : IMagicTypesRandomAffixesFactory
    {
        #region Methods
        public IMagicTypesRandomAffixes CreateMagicTypesRandomAffixes(Guid id, Guid magicTypeId, int minimumAffixes, int maximumAffixes)
        {
            Contract.Requires<ArgumentOutOfRangeException>(minimumAffixes >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(minimumAffixes <= maximumAffixes);
            Contract.Ensures(Contract.Result<IMagicTypesRandomAffixes>() != null);

            return default(IMagicTypesRandomAffixes);
        }
        #endregion
    }
}
