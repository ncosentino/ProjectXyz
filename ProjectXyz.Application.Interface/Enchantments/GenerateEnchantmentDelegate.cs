using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public delegate IEnchantment GenerateEnchantmentDelegate(IRandom randomizer, Guid enchantmentDefinitionId);
}
