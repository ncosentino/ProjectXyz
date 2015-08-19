using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentWeather
    {
        #region Properties
        Guid Id { get; }

        Guid EnchantmentId { get; }

        IEnumerable<Guid> WeatherIds { get; }
        #endregion
    }
}
