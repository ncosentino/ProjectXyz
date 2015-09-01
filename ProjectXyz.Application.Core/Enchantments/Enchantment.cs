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
        private readonly Guid _weatherTypeGroupingId;
        #endregion

        #region Constructors
        protected Enchantment(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            Guid enchantmentTypeId,
            Guid weatherTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(weatherTypeGroupingId != Guid.Empty);

            _id = id;
            _triggerId = triggerId;
            _statusTypeId = statusTypeId;
            _enchantmentTypeId = enchantmentTypeId;
            _weatherTypeGroupingId = weatherTypeGroupingId;
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
        public Guid WeatherGroupingId
        {
            get { return _weatherTypeGroupingId; }
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        public virtual void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            // do nothing
        }

        protected void OnExpired()
        {
            var handler = Expired;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
