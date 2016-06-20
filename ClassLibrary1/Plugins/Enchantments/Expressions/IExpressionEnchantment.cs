using System;
using System.Collections.Generic;
using ClassLibrary1.Application.Interface.Enchantments;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Plugins.Enchantments.Expressions
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
