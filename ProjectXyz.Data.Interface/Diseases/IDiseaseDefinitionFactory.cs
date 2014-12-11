using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseDefinitionFactory
    {
        #region Methods
        IDiseaseDefinition Create(Guid id, string name, Guid diseaseStatesId);
        #endregion
    }
}
