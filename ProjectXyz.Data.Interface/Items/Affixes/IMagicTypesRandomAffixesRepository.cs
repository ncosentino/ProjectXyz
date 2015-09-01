using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes.Contracts;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    [ContractClass(typeof(IMagicTypesRandomAffixesRepositoryContract))]
    public interface IMagicTypesRandomAffixesRepository
    {
        #region Methods
        IMagicTypesRandomAffixes GetForMagicTypeId(Guid id);

        IMagicTypesRandomAffixes Add(
            Guid id,
            Guid magicTypeId,
            int minimumAffixes,
            int maximumAffixes);

        void RemoveById(Guid id);

        IMagicTypesRandomAffixes GetById(Guid id);

        IEnumerable<IMagicTypesRandomAffixes> GetAll();
        #endregion
    }
}
