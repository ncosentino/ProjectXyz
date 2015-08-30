using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseStatesEnchantmentsRepository
    {
        #region Methods
        IDiseaseStatesEnchantments GetById(Guid id);

        IDiseaseStatesEnchantments GetByDiseaseStateId(Guid id);
        #endregion
    }
}
