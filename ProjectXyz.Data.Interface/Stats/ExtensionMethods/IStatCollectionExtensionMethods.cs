using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Stats.ExtensionMethods
{
    public static class IStatCollectionExtensionMethods
    {
        #region Methods
        public static double GetValueOrDefault(this IStatCollection stats, Guid statId, Func<double> defaultValueCallback)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(defaultValueCallback != null);

            return stats.Contains(statId)
                ? stats[statId].Value
                : defaultValueCallback.Invoke();
        }

        public static double GetValueOrDefault(this IStatCollection stats, Guid statId, double defaultValue)
        {
            Contract.Requires<ArgumentNullException>(stats != null);

            return stats.Contains(statId)
                ? stats[statId].Value
                : defaultValue;
        }
        #endregion
    }
}
