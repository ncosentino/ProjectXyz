using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantmentStat : IExpressionEnchantmentStat
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionEnchantmentStat"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="expressionEnchantmentId">The expression enchantment identifier.</param>
        /// <param name="idForExpression">The identifier for expression.</param>
        /// <param name="statId">The stat identifier.</param>
        private ExpressionEnchantmentStat(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionEnchantmentId != Guid.Empty);
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(idForExpression));
            Contract.Requires<ArgumentException>(statId != Guid.Empty);

            this.Id = id;
            this.ExpressionEnchantmentId = expressionEnchantmentId;
            this.IdForExpression = idForExpression;
            this.StatId = statId;
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
        public Guid ExpressionEnchantmentId
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string IdForExpression
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
        #endregion

        #region Methods
        public static IExpressionEnchantmentStat Create(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            Guid statId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionEnchantmentId != Guid.Empty);
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(idForExpression));
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStat>() != null);

            return new ExpressionEnchantmentStat(
                id,
                expressionEnchantmentId,
                idForExpression,
                statId);
        }
        #endregion
    }
}
