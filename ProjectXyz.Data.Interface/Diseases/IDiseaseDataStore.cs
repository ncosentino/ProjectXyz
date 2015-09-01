using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseDataStore
    {
        #region Properties
        IDiseaseDefinitionRepository DiseaseDefinitions { get; }

        IDiseaseSpreadTypeRepository DiseaseSpreadTypes { get; }

        IDiseaseStateDefinitionRepository DiseaseStateDefinitions { get; }

        IDiseaseStatesEnchantmentsRepository DiseaseStatesEnchantmentss { get; }
        #endregion
    }
}
