using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Time;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentContext
    {
        #region Properties
        Guid ActiveWeatherId { get; }

        ICalendar Calendar { get; }
        #endregion
    }
}
