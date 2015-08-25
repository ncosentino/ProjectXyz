using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentStatDefinitionRepositoryContract))]
    public interface IExpressionEnchantmentStatDefinitionRepository
    {
        #region Methods
        void Add(IExpressionEnchantmentStatDefinition expressionEnchantmentStatDefinition);

        void RemoveById(Guid id);

        IExpressionEnchantmentStatDefinition GetById(Guid id);

        IEnumerable<IExpressionEnchantmentStatDefinition> GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId);
        #endregion
    }
}
