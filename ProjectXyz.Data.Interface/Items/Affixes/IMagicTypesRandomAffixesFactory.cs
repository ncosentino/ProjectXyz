using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes.Contracts;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    [ContractClass(typeof(IMagicTypesRandomAffixesFactoryContract))]
    public interface IMagicTypesRandomAffixesFactory
    {
        #region Methods
        IMagicTypesRandomAffixes Create(Guid id, Guid magicTypeId, int minimumAffixes, int maximumAffixes);
        #endregion
    }
}
