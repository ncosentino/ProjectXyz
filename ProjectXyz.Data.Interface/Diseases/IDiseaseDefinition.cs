using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseDefinition
    {
        #region Properties
        Guid Id { get; }

        string Name { get; }

        Guid DiseaseStatesId { get; }
        #endregion
    }
}
