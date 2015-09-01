using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes.Contracts
{
    [ContractClassFor(typeof(IMagicTypesRandomAffixesRepository))]
    public abstract class IMagicTypesRandomAffixesRepositoryContract : IMagicTypesRandomAffixesRepository
    {
        #region Methods
        public IMagicTypesRandomAffixes GetForMagicTypeId(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IMagicTypesRandomAffixes>() != null);

            return default(IMagicTypesRandomAffixes);
        }

        public IMagicTypesRandomAffixes Add(Guid id, Guid magicTypeId, int minimumAffixes, int maximumAffixes)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumAffixes >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(maximumAffixes >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(maximumAffixes >= minimumAffixes);
            Contract.Ensures(Contract.Result<IMagicTypesRandomAffixes>() != null);

            return default(IMagicTypesRandomAffixes);
        }

        public void RemoveById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
        }

        public IMagicTypesRandomAffixes GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IMagicTypesRandomAffixes>() != null);

            return default(IMagicTypesRandomAffixes);
        }

        public IEnumerable<IMagicTypesRandomAffixes> GetAll()
        {
            Contract.Ensures(Contract.Result<IEnumerable<IMagicTypesRandomAffixes>>() != null);

            return default(IEnumerable<IMagicTypesRandomAffixes>);
        }
        #endregion
    }
}
