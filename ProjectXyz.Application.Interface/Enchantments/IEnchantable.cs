using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantable
    {
        #region Events
        event NotifyCollectionChangedEventHandler EnchantmentsChanged;
        #endregion

        #region Properties
        IEnchantmentCollection Enchantments { get; }
        #endregion

        #region Methods
        void Enchant(IEnumerable<IEnchantment> enchantments);

        void Disenchant(IEnumerable<IEnchantment> enchantments);
        #endregion
    }
}
