using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentValue
    {
        #region Properties
        Guid Id { get; }

        Guid ExpressionEnchantmentId { get; }

        string IdForExpression { get; }

        double Value { get; }
        #endregion
    }
}
