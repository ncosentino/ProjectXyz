using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentWeatherFactory
    {
        #region Methods
        IEnchantmentWeather Create(
            Guid id,
            Guid enchantmentId,
            IEnumerable<Guid> weatherIds);
        #endregion
    }
}
