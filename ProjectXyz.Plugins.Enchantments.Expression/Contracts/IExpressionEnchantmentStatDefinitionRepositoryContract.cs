using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentStatDefinitionRepository))]
    public abstract class IExpressionEnchantmentStatDefinitionRepositoryContract : IExpressionEnchantmentStatDefinitionRepository
    {
        #region Methods
        public IExpressionEnchantmentStatDefinition GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStatDefinition>() != null);

            return default(IExpressionEnchantmentStatDefinition);
        }

        public IEnumerable<IExpressionEnchantmentStatDefinition> GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnumerable<IExpressionEnchantmentStatDefinition>>() != null);

            return default(IEnumerable<IExpressionEnchantmentStatDefinition>);
        }

        public void Add(IExpressionEnchantmentStatDefinition expressionEnchantmentStatDefinition)
        {
            Contract.Requires<ArgumentNullException>(expressionEnchantmentStatDefinition != null);
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
