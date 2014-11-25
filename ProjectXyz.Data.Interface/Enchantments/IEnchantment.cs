using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentContract))]
    public interface IEnchantment
    {
        #region Properties
        Guid StatId { get; set; }

        Guid CalculationId { get; set; }

        Guid TriggerId { get; set; }

        Guid StatusTypeId { get; set; }

        TimeSpan RemainingDuration { get; set; }

        double Value { get; set; }
        #endregion
    }
}
