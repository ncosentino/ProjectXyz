using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentStoreContract))]
    public interface IEnchantmentStore
    {
        #region Properties
        Guid StatId { get; }

        Guid CalculationId { get; }

        Guid TriggerId { get; }

        Guid StatusTypeId { get; }

        TimeSpan RemainingDuration { get; }

        double Value { get; }
        #endregion
    }
}
