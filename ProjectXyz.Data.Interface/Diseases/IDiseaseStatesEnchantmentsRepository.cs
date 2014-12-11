using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseStatesEnchantmentsRepository
    {
        #region Methods
        IDiseaseStatesEnchantments GetById(Guid id);
        #endregion
    }
}
