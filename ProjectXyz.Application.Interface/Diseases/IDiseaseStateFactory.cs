using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Diseases
{
    public interface IDiseaseStateFactory
    {
        #region Methods
        IDiseaseState Create(
            Guid id, 
            Guid nameStringResourceId, 
            Guid previousStateId, 
            Guid nextStateId, 
            Guid diseaseSpreadTypeId, 
            IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
