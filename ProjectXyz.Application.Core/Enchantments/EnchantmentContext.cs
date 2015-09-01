using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Time;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentContext : IEnchantmentContext
    {
        #region Constructors
        private EnchantmentContext()
        {
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid ActiveWeatherId
        {
	        get { throw new NotImplementedException(); }
        }

        /// <inheritdoc />
        public ICalendar Calendar
        {
	        get { throw new NotImplementedException(); }
        }
        #endregion

        #region Methods
        public static IEnchantmentContext Create()
        {
            var context = new EnchantmentContext();
            return context;
        }
        #endregion
    }
}
