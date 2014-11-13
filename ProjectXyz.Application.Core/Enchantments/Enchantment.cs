using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class Enchantment : IEnchantment
    {
        #region Fields
        private readonly IEnchantmentContext _context;
        private readonly ProjectXyz.Data.Interface.Enchantments.IEnchantment _enchantment;
        #endregion

        #region Constructors
        private Enchantment(IEnchantmentContext context, ProjectXyz.Data.Interface.Enchantments.IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);

            _context = context;
            _enchantment = enchantment;
        }
        #endregion

        #region Properties
        public string StatId
        {
            get { return _enchantment.StatId; }
        }

        public double Value
        {
            get { return _enchantment.Value; }
        }

        public string CalculationId
        {
            get { return _enchantment.CalculationId; }
        }

        public string Trigger
        {
            get { return _enchantment.Trigger; }
        }

        public string StatusType
        {
            get { return _enchantment.StatusType; }
        }

        public TimeSpan RemainingDuration
        {
            get { return _enchantment.RemainingDuration; }
        }
        #endregion

        #region Methods
        public static IEnchantment Create(IEnchantmentContext context, ProjectXyz.Data.Interface.Enchantments.IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);
            Contract.Ensures(Contract.Result<IEnchantment>() != null);
            return new Enchantment(context, enchantment);
        }

        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            if (_enchantment.HasInfiniteDuration())
            {
                return;
            }

            _enchantment.RemainingDuration = TimeSpan.FromMilliseconds(Math.Max(
                (_enchantment.RemainingDuration - elapsedTime).TotalMilliseconds,
                0));
        }
        #endregion
    }
}
