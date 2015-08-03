using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantable
    {
        #region Properties
        IObservableEnchantmentCollection Enchantments { get; }
        #endregion

        #region Methods
        void Enchant(IEnumerable<IEnchantment> enchantments);

        void Disenchant(IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
