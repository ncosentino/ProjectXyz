using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Stats.ExtensionMethods
{
    public static class IStatCollectionExtensionMethods
    {
        #region Methods
        public static double GetValueOrDefault<TStat>(this IStatCollection<TStat> stats, string statId, Func<double> defaultValueCallback) where TStat : IStat
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(statId != null);
            Contract.Requires<ArgumentException>(statId != string.Empty);
            Contract.Requires<ArgumentNullException>(defaultValueCallback != null);

            return stats.Contains(statId)
                ? stats[statId].Value
                : defaultValueCallback.Invoke();
        }

        public static double GetValueOrDefault<TStat>(this IStatCollection<TStat> stats, string statId, double defaultValue) where TStat : IStat
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(statId != null);
            Contract.Requires<ArgumentException>(statId != string.Empty);

            return stats.Contains(statId)
                ? stats[statId].Value
                : defaultValue;
        }
        #endregion
    }
}
