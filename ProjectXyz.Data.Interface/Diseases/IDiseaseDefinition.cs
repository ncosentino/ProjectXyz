using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Diseases.Contracts;

namespace ProjectXyz.Data.Interface.Diseases
{
    [ContractClass(typeof(IDiseaseDefinitionContract))]
    public interface IDiseaseDefinition
    {
        #region Properties
        Guid Id { get; }

        Guid NameStringResourceId { get; }

        Guid DiseaseStatesId { get; }
        #endregion
    }
}
