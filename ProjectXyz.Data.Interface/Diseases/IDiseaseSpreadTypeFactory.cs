using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Diseases.Contracts;

namespace ProjectXyz.Data.Interface.Diseases
{
    [ContractClass(typeof(IDiseaseSpreadTypeFactoryContract))]
    public interface IDiseaseSpreadTypeFactory
    {
        #region Methods
        IDiseaseSpreadType Create(Guid id, string name);
        #endregion
    }
}
