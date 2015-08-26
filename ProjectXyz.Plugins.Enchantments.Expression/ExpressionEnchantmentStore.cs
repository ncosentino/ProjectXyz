using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantmentStore : IExpressionEnchantmentStore
    {
        #region Constructors
        private ExpressionEnchantmentStore(
            Guid id,
            Guid statId,
            Guid expressionId,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);

            this.Id = id;
            this.StatId = statId;
            this.ExpressionId = expressionId;
            this.RemainingDuration = remainingDuration;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Guid StatId
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Guid ExpressionId
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public TimeSpan RemainingDuration
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentStore Create(
            Guid id,
            Guid statId,
            Guid expressionId,
            TimeSpan remainingDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStore>() != null);

            return new ExpressionEnchantmentStore(
                id,
                statId,
                expressionId,
                remainingDuration);
        }
        #endregion
    }
}
