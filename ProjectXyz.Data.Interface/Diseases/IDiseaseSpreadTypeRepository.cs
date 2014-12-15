using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Diseases.Contracts;

namespace ProjectXyz.Data.Interface.Diseases
{
    [ContractClass(typeof(IDiseaseSpreadTypeRepositoryContract))]
    public interface IDiseaseSpreadTypeRepository
    {
        #region Methods
        IDiseaseSpreadType GetById(Guid id);
        #endregion
    }
}
