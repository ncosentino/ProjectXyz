using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseStateDefinition
    {
        #region Properties
        Guid Id { get; }

        string Name { get; }

        Guid PreviousStateId { get; }

        Guid NextStateId { get; }

        Guid DiseaseStatesEnchantmentsId { get; }

        Guid DiseaseSpreadTypeId { get; }
        #endregion
    }
}
