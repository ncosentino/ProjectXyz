using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Diseases.Contracts
{
    [ContractClassFor(typeof(IDiseaseSpreadTypeRepository))]
    public abstract class IDiseaseSpreadTypeRepositoryContract : IDiseaseSpreadTypeRepository
    {
        #region Methods
        public IDiseaseSpreadType GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IDiseaseSpreadType>() != null);

            return default(IDiseaseSpreadType);
        }
        #endregion
    }
}
