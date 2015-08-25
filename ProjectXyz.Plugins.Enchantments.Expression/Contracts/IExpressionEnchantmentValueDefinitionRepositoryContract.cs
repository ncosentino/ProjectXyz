using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression.Contracts
{
    [ContractClassFor(typeof(IExpressionEnchantmentValueDefinitionRepository))]
    public abstract class IExpressionEnchantmentValueDefinitionRepositoryContract : IExpressionEnchantmentValueDefinitionRepository
    {
        #region Methods
        public IExpressionEnchantmentValueDefinition GetById(Guid id)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValueDefinition>() != null);

            return default(IExpressionEnchantmentValueDefinition);
        }

        public IEnumerable<IExpressionEnchantmentValueDefinition> GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<IEnumerable<IExpressionEnchantmentValueDefinition>>() != null);

            return default(IEnumerable<IExpressionEnchantmentValueDefinition>);
        }

        public void Add(IExpressionEnchantmentValueDefinition expressionEnchantmentValueDefinition)
        {
            Contract.Requires<ArgumentNullException>(expressionEnchantmentValueDefinition != null);
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
