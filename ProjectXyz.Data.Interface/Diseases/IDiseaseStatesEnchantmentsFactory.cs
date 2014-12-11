using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseStatesEnchantmentsFactory
    {
        #region Methods
        IDiseaseStatesEnchantments Create(Guid id, Guid diseaseStateId, IEnumerable<Guid> enchantmentIds);
        #endregion
    }
}
