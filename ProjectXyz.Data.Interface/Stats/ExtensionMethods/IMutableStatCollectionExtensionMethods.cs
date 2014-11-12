using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Stats.ExtensionMethods
{
    public static class IMutableStatCollectionExtensionMethods
    {
        #region Methods
        public static void Add(this IMutableStatCollection stats, IStat stat)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(stat != null);

            stats.Add(new IStat[] { stat });
        }

        public static bool Remove(this IMutableStatCollection stats, IStat stat)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Requires<ArgumentNullException>(stat != null);

            return stats.Remove(new IStat[] { stat });
        }
        #endregion
    }
}
