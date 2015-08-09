using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentGenerator : IRegisterCallbackForType<IAdditiveEnchantmentDefinition, GenerateEnchantmentDelegate>
    {
        #region Methods
        IEnchantment Generate(IRandom randomizer, Guid enchantmentId);
        #endregion
    }
}
