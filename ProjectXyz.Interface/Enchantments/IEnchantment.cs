using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Interface.Enchantments.Contracts;

namespace ProjectXyz.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentContract))]
    public interface IEnchantment
    {
        #region Properties
        string StatId { get; set; }

        double Value { get; set; }

        string CalculationId { get; set; }

        TimeSpan RemainingDuration { get; set; }
        #endregion
    }
}
