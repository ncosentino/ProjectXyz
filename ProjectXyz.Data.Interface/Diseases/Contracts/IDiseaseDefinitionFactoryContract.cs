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
        public IDiseaseDefinition Create(Guid id, string name, Guid diseaseStatesId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);
            Contract.Requires<ArgumentException>(diseaseStatesId != Guid.Empty);
            Contract.Ensures(Contract.Result<IDiseaseDefinition>() != null);

            return default(IDiseaseDefinition);
        }
        #endregion
    }
}
