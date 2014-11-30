using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class Enchantment : IEnchantment
    {
        #region Fields
        private readonly IEnchantmentContext _context;
        private readonly Guid _statId;
        private readonly double _value; 
        private readonly Guid _calculationId; 
        private readonly Guid _triggerId; 
        private readonly Guid _statusTypeId;
        
        private TimeSpan _remainingDuration;
        #endregion

        #region Constructors
        private Enchantment(
            IEnchantmentContext context,
            Guid statId,
            Guid statusTypeId,
            Guid triggerId,
            Guid calculationId,
            double value,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(remainingDuration >= TimeSpan.Zero);

            _context = context;
            _statId = statId;
            _value = value;
            _calculationId = calculationId;
            _triggerId = triggerId;
            _statusTypeId = statusTypeId;
            _remainingDuration = remainingDuration;
        }
        #endregion

        #region Properties
        public Guid StatId
        {
            get { return _statId; }
        }

        public double Value
        {
            get { return _value; }
        }

        public Guid CalculationId
        {
            get { return _calculationId; }
        }

        public Guid TriggerId
        {
            get { return _triggerId; }
        }

        public Guid StatusTypeId
        {
            get { return _statusTypeId; }
        }

        public TimeSpan RemainingDuration
        {
            get { return _remainingDuration; }
        }
        #endregion

        #region Methods
        public static IEnchantment Create(IEnchantmentContext context, IEnchantmentStore enchantment)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(enchantment != null);
            Contract.Ensures(Contract.Result<IEnchantment>() != null);
            
            return Create(
                context, 
                enchantment.StatId,
                enchantment.StatusTypeId,
                enchantment.TriggerId,
                enchantment.CalculationId,
                enchantment.Value,
                enchantment.RemainingDuration);
        }

        public static IEnchantment Create(
            IEnchantmentContext context, 
            Guid statId, 
            Guid statusTypeId, 
            Guid triggerId, 
            Guid calculationId, 
            double value,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IEnchantment>() != null);

            return new Enchantment(
                context, 
                statId,
                statusTypeId,
                triggerId,
                calculationId,
                value,
                remainingDuration);
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
