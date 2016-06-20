using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public interface IExpressionEnchantment : IEnchantment
    {
        #region Properties
        IIdentifier StatDefinitionId { get; }

        string Expression { get; }

        int CalculationPriority { get; }

        IEnumerable<string> StatExpressionIds { get; }

        IEnumerable<string> ValueExpressionIds { get; }

        IIdentifier WeatherGroupingId { get; }
        #endregion

        #region Methods
        IIdentifier GetStatDefinitionIdForStatExpressionId(string statExpressionId);

        double GetValueForValueExpressionId(string valueExpressionId);
        #endregion
    }
}
