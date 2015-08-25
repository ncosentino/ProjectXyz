using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantmentValue : IExpressionEnchantmentValue
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionEnchantmentValue"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="expressionEnchantmentId">The expression enchantment identifier.</param>
        /// <param name="idForExpression">The identifier for expression.</param>
        /// <param name="value">The value.</param>
        private ExpressionEnchantmentValue(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionEnchantmentId != Guid.Empty);
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(idForExpression));

            this.Id = id;
            this.ExpressionEnchantmentId = expressionEnchantmentId;
            this.IdForExpression = idForExpression;
            this.Value = value;
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
        public double Value
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentValue Create(
            Guid id,
            Guid expressionEnchantmentId,
            string idForExpression,
            double value)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(expressionEnchantmentId != Guid.Empty);
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(idForExpression));
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValue>() != null);

            return new ExpressionEnchantmentValue(
                id,
                expressionEnchantmentId,
                idForExpression,
                value);
        }
        #endregion
    }
}
