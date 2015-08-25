using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentValueDefinitionRepositoryContract))]
    public interface IExpressionEnchantmentValueDefinitionRepository
    {
        #region Methods
        void Add(IExpressionEnchantmentValueDefinition expressionEnchantmentValueDefinition);

        void RemoveById(Guid id);

        IExpressionEnchantmentValueDefinition GetById(Guid id);

        IEnumerable<IExpressionEnchantmentValueDefinition> GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId);
        #endregion
    }
}
