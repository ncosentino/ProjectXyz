using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseStateDefinitionRepository
    {
        #region Methods
        IDiseaseStateDefinition GetById(Guid id);
        #endregion
    }
}
