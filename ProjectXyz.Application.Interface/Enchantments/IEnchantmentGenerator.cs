using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentGenerator
    {
        #region Methods
        IEnchantment Generate(IRandom randomizer, Guid enchantmentId);
        #endregion
    }
}
