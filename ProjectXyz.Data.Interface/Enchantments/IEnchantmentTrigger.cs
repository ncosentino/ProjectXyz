﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentTriggerContract))]
    public interface IEnchantmentTrigger
    {
        #region Properties
        Guid Id { get; }

        string Name { get; }
        #endregion
    }
}