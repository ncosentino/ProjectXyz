using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Diseases.Contracts;

namespace ProjectXyz.Data.Interface.Diseases
{
    [ContractClass(typeof(IDiseaseDefinitionFactoryContract))]
    public interface IDiseaseDefinitionFactory
    {
        #region Methods
        IDiseaseDefinition Create(
            Guid id, 
            Guid nameStringResourceId,
            Guid diseaseStatesId);
        #endregion
    }
}
