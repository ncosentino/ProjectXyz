using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseStateDefinitionFactory
    {
        #region Methods
        IDiseaseStateDefinition Create(
            Guid id,
            string name,
            Guid previousStateId, 
            Guid nextStateId, 
            Guid diseaseStatesEnchantmentsId, 
            Guid diseaseSpreadTypeId);
        #endregion
    }
}
