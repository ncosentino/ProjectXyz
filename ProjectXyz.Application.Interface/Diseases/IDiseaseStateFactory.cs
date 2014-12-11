using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Diseases
{
    public interface IDiseaseStateFactory
    {
        #region Methods
        IDiseaseState Create(
            Guid id, 
            string name, 
            Guid previousStateId, 
            Guid nextStateId, 
            Guid diseaseSpreadTypeId, 
            IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
