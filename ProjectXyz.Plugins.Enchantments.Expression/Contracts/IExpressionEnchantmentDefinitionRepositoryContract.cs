using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentDefinitionRepository))]
    public abstract class IExpressionEnchantmentDefinitionRepositoryContract : IExpressionEnchantmentDefinitionRepository
    {
        #region Methods
        public IExpressionEnchantmentDefinition GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentDefinition>() != null);

            return default(IExpressionEnchantmentDefinition);
        }

        public IExpressionEnchantmentDefinition GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentDefinition>() != null);

            return default(IExpressionEnchantmentDefinition);
        }

        public void Add(IExpressionEnchantmentDefinition expressionEnchantmentDefinition)
        {
            Contract.Requires<ArgumentNullException>(expressionEnchantmentDefinition != null);
        }

        public void RemoveById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
        }

        public void RemoveByEnchantmentDefinitionId(Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
        }
        #endregion
    }
}
