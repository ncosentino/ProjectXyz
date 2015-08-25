using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Plugins.Enchantments.Expression.Contracts;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    [ContractClass(typeof(IExpressionEnchantmentStatRepositoryContract))]
    public interface IExpressionEnchantmentStatRepository
    {
        #region Methods
        void Add(IExpressionEnchantmentStat expressionEnchantmentStat);

        void RemoveById(Guid id);

        void RemoveByExpressionEnchantmentId(Guid expressionEnchantmentId);

        IExpressionEnchantmentStat GetById(Guid id);

        IEnumerable<IExpressionEnchantmentStat> GetByExpressionEnchantmentId(Guid expressionEnchantmentId);
        #endregion
    }
}
