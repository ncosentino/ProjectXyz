using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseSpreadTypeRepository
    {
        #region Methods
        IDiseaseSpreadType GetById(Guid id);
        #endregion
    }
}
