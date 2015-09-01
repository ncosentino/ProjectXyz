using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments.Contracts;

namespace ProjectXyz.Application.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentContract))]
    public interface IEnchantment : IUpdateElapsedTime
    {
        #region Events
        event EventHandler<EventArgs> Expired;
        #endregion

        #region Properties
        Guid Id { get; }

        Guid TriggerId { get; }

        Guid StatusTypeId { get; }

        Guid EnchantmentTypeId { get; }

        Guid WeatherGroupingId { get; }
        #endregion
    }
}
