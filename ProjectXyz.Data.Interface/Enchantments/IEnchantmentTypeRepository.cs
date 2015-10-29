using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentTypeRepository
    {
        #region Methods
        IEnchantmentType Add(
            Guid id,
            Guid nameStringResourceId);

        IEnchantmentType GetById(Guid id);
        
        IEnumerable<IEnchantmentType> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
