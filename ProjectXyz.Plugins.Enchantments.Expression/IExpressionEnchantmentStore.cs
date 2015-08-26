using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantmentStore
    {
        #region Properties
        Guid Id { get; }

        Guid StatId { get; }

        Guid ExpressionId { get; }

        TimeSpan RemainingDuration { get; }
        #endregion
    }
}
