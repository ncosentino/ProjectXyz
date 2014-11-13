using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class Enchantment : IEnchantment
    {
        #region Fields
        private readonly IEnchantmentContext _context;
        private readonly string _statId;
        private readonly double _value; 
        private readonly string _calculationId; 
        private readonly string _trigger; 
        private readonly string _statusType;
        
        private TimeSpan _remainingDuration;
        #endregion

        #region Constructors
        private Enchantment(IEnchantmentContext context, ProjectXyz.Data.Interface.Enchantments.IEnchantment enchantment)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);

            _context = context;
            _statId = enchantment.StatId;
            _value = enchantment.Value;
            _calculationId = enchantment.CalculationId;
            _trigger = enchantment.Trigger;
            _statusType = enchantment.StatusType;
            _remainingDuration = enchantment.RemainingDuration;
        }
        #endregion

        #region Properties
        public string StatId
        {
            get { return _statId; }
        }

        public double Value
        {
            get { return _value; }
        }

        public string CalculationId
        {
            get { return _calculationId; }
        }

        public string Trigger
        {
            get { return _trigger; }
        }

        public string StatusType
        {
            get { return _statusType; }
        }

        public TimeSpan RemainingDuration
        {
            get { return _remainingDuration; }
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
            if (this.HasInfiniteDuration())
            {
                return;
            }

            _remainingDuration = TimeSpan.FromMilliseconds(Math.Max(
                (_remainingDuration - elapsedTime).TotalMilliseconds,
                0));
        }
        #endregion
    }
}
