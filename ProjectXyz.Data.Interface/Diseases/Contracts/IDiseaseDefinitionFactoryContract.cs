using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Diseases.Contracts
{
    [ContractClassFor(typeof(IDiseaseDefinitionFactory))]
    public abstract class IDiseaseDefinitionFactoryContract : IDiseaseDefinitionFactory
    {
        #region Methods
        public IDiseaseDefinition Create(
            Guid id, 
            Guid nameStringResourceId, 
            Guid diseaseStatesId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(diseaseStatesId != Guid.Empty);
            Contract.Ensures(Contract.Result<IDiseaseDefinition>() != null);

            return default(IDiseaseDefinition);
        }
        #endregion
    }
}
