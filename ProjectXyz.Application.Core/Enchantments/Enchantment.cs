using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Core.Enchantments
{
    public abstract class Enchantment : IEnchantment
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _triggerId; 
        private readonly Guid _statusTypeId;
        private readonly Guid _enchantmentTypeId;
        private readonly List<Guid> _weatherIds;

        private TimeSpan _remainingDuration;
        #endregion

        #region Constructors
        protected Enchantment(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            Guid enchantmentTypeId,
            IEnumerable<Guid> weatherIds)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentNullException>(weatherIds != null);

            _id = id;
            _triggerId = triggerId;
            _statusTypeId = statusTypeId;
            _enchantmentTypeId = enchantmentTypeId;
            _weatherIds = new List<Guid>(weatherIds);
        }
        #endregion

        #region Events
        /// <inheritdoc />
        public event EventHandler<EventArgs> Expired;
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
        public Guid EnchantmentTypeId
        {
            get { return _enchantmentTypeId; }
        }

        /// <inheritdoc />
        public IEnumerable<Guid> WeatherIds
        {
            get { return _weatherIds; }
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            if (_remainingDuration == TimeSpan.Zero)
            {
                return;
            }

            _remainingDuration = TimeSpan.FromMilliseconds(Math.Max(
                (_remainingDuration - elapsedTime).TotalMilliseconds,
                0));

            if (_remainingDuration == TimeSpan.Zero)
            {
                var handler = Expired;
                if (handler != null)
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion
    }
}
