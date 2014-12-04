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
        Guid Id { get; }

        Guid StatId { get; }

        Guid CalculationId { get; }

        Guid TriggerId { get; }

        Guid StatusTypeId { get; }

        TimeSpan RemainingDuration { get; }

        double Value { get; }
        #endregion
    }
}
