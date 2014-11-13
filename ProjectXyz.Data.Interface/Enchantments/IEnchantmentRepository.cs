using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentRepository
    {
        #region Methods
        IEnumerable<IEnchantment> GenerateRandom(int minimum, int maximum, int level, Random randomizer);

        IEnchantment Generate(Guid id, Random randomizer);
        #endregion
    }
}
