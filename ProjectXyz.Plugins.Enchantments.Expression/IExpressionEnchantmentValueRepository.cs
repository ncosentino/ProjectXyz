using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentValueRepositoryContract))]
    public interface IExpressionEnchantmentValueRepository
    {
        #region Methods
        void Add(IExpressionEnchantmentValue expressionEnchantmentValue);

        void RemoveById(Guid id);

        void RemoveByExpressionEnchantmentId(Guid expressionEnchantmentId);

        IExpressionEnchantmentValue GetById(Guid id);

        IEnumerable<IExpressionEnchantmentValue> GetByExpressionEnchantmentId(Guid expressionEnchantmentId);
        #endregion
    }
}
