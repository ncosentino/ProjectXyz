using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate
{
    public sealed class OneShotNegateEnchantmentDefinition : IOneShotNegateEnchantmentDefinition
    {
        #region Constructors
        private OneShotNegateEnchantmentDefinition(
            Guid id,
            Guid statId,
            Guid triggerId,
            Guid statusTypeId)
        {
            this.Id = id;
            this.StatId = statId;
            this.TriggerId = triggerId;
            this.StatusTypeId = statusTypeId;
        }
        #endregion

        #region Properties
        public Guid Id { get; set; }

        public Guid StatId { get; set; }

        public Guid TriggerId { get; set; }

        public Guid StatusTypeId { get; set; }

        public TimeSpan MinimumDuration { get; set; }

        public TimeSpan MaximumDuration { get; set; }
        #endregion

        #region Methods
        public static IEnchantmentDefinition Create(
            Guid id,
            Guid statId,
            Guid triggerId,
            Guid statusTypeId)
        {
            Contract.Ensures(Contract.Result<IEnchantmentDefinition>() != null);

            return new OneShotNegateEnchantmentDefinition(
                id,
                statId,
                triggerId,
                statusTypeId);
        }
        #endregion
    }
}
