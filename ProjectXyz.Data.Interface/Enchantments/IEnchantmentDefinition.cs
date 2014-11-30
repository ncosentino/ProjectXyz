using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentDefinition
    {
        #region Properties
        Guid Id { get; set; }
        
        Guid StatId { get; set; }

        Guid CalculationId { get; set; }

        Guid TriggerId { get; set; }

        Guid StatusTypeId { get; set; }

        TimeSpan MinimumDuration { get; set; }
        
        TimeSpan MaximumDuration { get; set; }

        double MinimumValue { get; set; }

        double MaximumValue { get; set; }
        #endregion
    }
}