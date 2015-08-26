using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionDefinitionRepository))]
    public abstract class IExpressionDefinitionRepositoryContract : IExpressionDefinitionRepository
    {
        #region Methods
        public IExpressionDefinition GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionDefinition>() != null);

            return default(IExpressionDefinition);
        }

        public void Add(IExpressionDefinition expressionDefinition)
        {
            Contract.Requires<ArgumentNullException>(expressionDefinition != null);
        }

        public void RemoveById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
        }
        #endregion
    }
}
