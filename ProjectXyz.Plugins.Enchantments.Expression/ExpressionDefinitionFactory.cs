using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionDefinitionFactory : IExpressionDefinitionFactory
    {
        #region Constructors
        private ExpressionDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IExpressionDefinitionFactory Create()
        {
            var factory = new ExpressionDefinitionFactory();
            return factory;
        }

        public IExpressionDefinition CreateExpressionDefinition(
            Guid id,
            string expression,
            int calculationPriority)
        {
            var expressionDefinition = ExpressionDefinition.Create(
                id,
                expression,
                calculationPriority);
            return expressionDefinition;
        }
        #endregion
    }
}
