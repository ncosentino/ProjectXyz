using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments.Contracts;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentContract))]
    public interface IEnchantment : IUpdateElapsedTime
    {
        #region Properties
        string StatId { get; }

        double Value { get; }

        string CalculationId { get; }

        Guid TriggerId { get; }

        string StatusType { get; }

        TimeSpan RemainingDuration { get; }
        #endregion
    }
}
