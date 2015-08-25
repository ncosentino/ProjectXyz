using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentStat
    {
        #region Properties
        Guid Id { get; }

        Guid ExpressionEnchantmentId { get; }

        string IdForExpression { get; }

        Guid StatId { get; }
        #endregion
    }
}
