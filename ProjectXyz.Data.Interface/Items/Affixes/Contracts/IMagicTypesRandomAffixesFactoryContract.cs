using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes.Contracts
{
    [ContractClassFor(typeof(IMagicTypesRandomAffixesFactory))]
    public abstract class IMagicTypesRandomAffixesFactoryContract : IMagicTypesRandomAffixesFactory
    {
        #region Methods
        public IMagicTypesRandomAffixes Create(Guid id, Guid magicTypeId, int minimumAffixes, int maximumAffixes)
        {
            Contract.Requires<ArgumentOutOfRangeException>(minimumAffixes >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(minimumAffixes <= maximumAffixes);
            Contract.Ensures(Contract.Result<IMagicTypesRandomAffixes>() != null);

            return default(IMagicTypesRandomAffixes);
        }
        #endregion
    }
}
