using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantmentDefinition : IExpressionEnchantmentDefinition
    {
        #region Constructors
        private ExpressionEnchantmentDefinition(
            Guid id,
            Guid enchantmentDefinitionId,
            Guid expressionId,
            Guid statId,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(maximumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration <= maximumDuration);

            this.Id = id;
            this.EnchantmentDefinitionId = enchantmentDefinitionId;
            this.ExpressionId = expressionId;
            this.StatId = statId;
            this.MinimumDuration = minimumDuration;
            this.MaximumDuration = maximumDuration;
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
        public Guid EnchantmentDefinitionId
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
        public Guid StatId
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public TimeSpan MinimumDuration
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public TimeSpan MaximumDuration
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentDefinition Create(
            Guid id,
            Guid enchantmentDefinitionId,
            Guid expressionId,
            Guid statId,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(maximumDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentOutOfRangeException>(minimumDuration <= maximumDuration);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentDefinition>() != null);

            var enchantmentDefinition = new ExpressionEnchantmentDefinition(
                id,
                enchantmentDefinitionId,
                expressionId,
                statId,
                minimumDuration,
                maximumDuration);
            return enchantmentDefinition;
        }
        #endregion
    }
}
