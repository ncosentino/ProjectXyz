using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionDefinition : IExpressionDefinition
    {
        #region Constructors
        private ExpressionDefinition(
            Guid id,
            string expression,
            int calculationPriority)
        {
            this.Id = id;
            this.Expression = expression;
            this.CalculationPriority = calculationPriority;
        }
        #endregion

        #region Properties        
        /// <inheritdoc />
        public Guid Id { get; private set; }

        /// <inheritdoc />
        public string Expression { get; private set; }

        /// <inheritdoc />
        public int CalculationPriority { get; private set; }
        #endregion

        #region Methods
        public static IExpressionDefinition Create(
            Guid id,
            string expression,
            int calculationPriority)
        {
            var expressionDefinition = new ExpressionDefinition(
                id,
                expression,
                calculationPriority);
            return expressionDefinition;
        }
        #endregion
    }
}