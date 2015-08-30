using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Diseases.Contracts
{
    [ContractClassFor(typeof(IDiseaseDefinition))]
    public abstract class IDiseaseDefinitionContract : IDiseaseDefinition
    {
        #region Properties
        public Guid Id
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                
                return default(Guid);
            }
        }

        public Guid NameStringResourceId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);

                return default(Guid);
            }
        }

        public Guid DiseaseStatesId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);

                return default(Guid);
            }
        }
        #endregion
    }
}
