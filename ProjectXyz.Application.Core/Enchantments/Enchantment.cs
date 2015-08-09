using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Enchantments.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public abstract class Enchantment : IEnchantment
    {
        #region Fields
        private readonly IEnchantmentContext _context;
        private readonly Guid _id;
        private readonly Guid _triggerId; 
        private readonly Guid _statusTypeId;
        
        private TimeSpan _remainingDuration;
        #endregion

        #region Constructors
        protected Enchantment(
            IEnchantmentContext context,
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(remainingDuration >= TimeSpan.Zero);

            _context = context;
            _id = id;
            _triggerId = triggerId;
            _statusTypeId = statusTypeId;
            _remainingDuration = remainingDuration;
        }
        #endregion

        #region Properties        
        /// <inheritdoc />
        public Guid Id
        {
            get { return _id; }
        }

        /// <inheritdoc />
        public Guid TriggerId
        {
            get { return _triggerId; }
        }

        /// <inheritdoc />
        public Guid StatusTypeId
        {
            get { return _statusTypeId; }
        }

        /// <inheritdoc />
        public TimeSpan RemainingDuration
        {
            get { return _remainingDuration; }
        }
        #endregion

        #region Methods
        /// <inheritdoc />
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
