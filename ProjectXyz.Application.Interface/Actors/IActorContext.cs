using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Actors
{
    public interface IActorContext
    {
        #region Properties
        IEnchantmentCalculator EnchantmentCalculator { get; }
        #endregion
    }
}
