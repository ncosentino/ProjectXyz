using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Diseases.Contracts;

namespace ProjectXyz.Data.Interface.Diseases
{
    [ContractClass(typeof(IDiseaseDefinitionContract))]
    public interface IDiseaseDefinition
    {
        #region Properties
        Guid Id { get; }

        string Name { get; }

        Guid DiseaseStatesId { get; }
        #endregion
    }
}
