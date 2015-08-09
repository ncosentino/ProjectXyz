using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentSaver : 
        ISave<IEnchantment, IEnchantmentStore>,
        IRegisterCallbackForType<IEnchantmentStore, SaveEnchantmentDelegate>
    {
    }
}
