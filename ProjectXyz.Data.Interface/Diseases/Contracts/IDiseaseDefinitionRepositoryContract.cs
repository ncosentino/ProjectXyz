using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Diseases.Contracts
{
    [ContractClassFor(typeof(IDiseaseDefinitionRepository))]
    public abstract class IDiseaseDefinitionRepositoryContract : IDiseaseDefinitionRepository
    {
        #region Methods
        public IDiseaseDefinition GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IDiseaseDefinition>() != null);

            return default(IDiseaseDefinition);
        }
        #endregion
    }
}
