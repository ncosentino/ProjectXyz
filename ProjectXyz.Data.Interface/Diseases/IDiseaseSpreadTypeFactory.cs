using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseSpreadTypeFactory
    {
        #region Methods
        IDiseaseSpreadType Create(Guid id, string name);
        #endregion
    }
}
