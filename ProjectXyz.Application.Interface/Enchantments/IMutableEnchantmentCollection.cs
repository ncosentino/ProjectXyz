﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments.Contracts;

namespace ProjectXyz.Application.Interface.Enchantments
{
    [ContractClass(typeof(IMutableEnchantmentCollectionContract))]
    public interface IMutableEnchantmentCollection : IEnchantmentCollection
    {
        #region Methods
        void Add(IEnumerable<IEnchantment> enchantments);

        bool Remove(IEnumerable<IEnchantment> enchantments);

        void Clear();
        #endregion
    }
}
