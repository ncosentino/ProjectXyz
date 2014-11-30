using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentSaver : ISave<IEnchantment, ProjectXyz.Data.Interface.Enchantments.IEnchantmentStore>
    {
    }
}
