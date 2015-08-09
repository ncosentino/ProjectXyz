using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public delegate IEnchantmentStore SaveEnchantmentDelegate(IEnchantment enchantment);
}
