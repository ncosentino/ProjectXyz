using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public interface IExpressionEnchantment : IEnchantment
    {
        #region Properties
        Guid StatId { get; }

        string Expression { get; }

        IEnumerable<string> StatExpressionIds { get; }

        IEnumerable<string> ValueExpressionIds { get; }

        TimeSpan RemainingDuration { get; }
        #endregion

        #region Methods
        Guid GetStatIdForStatExpressionId(string statExpressionId);

        double GetValueForValueExpressionId(string valueExpressionId);
        #endregion
    }
}
