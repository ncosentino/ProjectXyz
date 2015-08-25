using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentDefinitionRepositoryContract))]
    public interface IExpressionEnchantmentDefinitionRepository
    {
        #region Methods
        void Add(IExpressionEnchantmentDefinition expressionEnchantmentDefinition);

        void RemoveById(Guid id);

        void RemoveByEnchantmentDefinitionId(Guid enchantmentDefinitionId);

        IExpressionEnchantmentDefinition GetById(Guid id);

        IExpressionEnchantmentDefinition GetByEnchantmentDefinitionId(Guid id);
        #endregion
    }
}
