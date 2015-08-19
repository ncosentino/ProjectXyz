using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentWeatherRepository
    {
        #region Methods
        void Add(IEnchantmentWeather enchantmentWeather);

        void RemoveById(Guid id);

        IEnchantmentWeather GetById(Guid id);
        #endregion
    }
}
