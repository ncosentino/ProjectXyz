using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionDefinitionRepositoryContract))]
    public interface IExpressionDefinitionRepository
    {
        #region Methods
        void Add(IExpressionDefinition expressionDefinition);

        void RemoveById(Guid id);

        IExpressionDefinition GetById(Guid id);
        #endregion
    }
}
