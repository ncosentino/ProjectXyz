using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Interface.Items.ExtensionMethods
{
    public static class IDurabilityExtensionMethods
    {
        #region Methods
        public static bool IsIndestructible(this IDurability durability)
        {
            Contract.Requires(durability != null);
            return durability.Maximum == 0;
        }

        public static bool IsBroken(this IDurability durability)
        {
            Contract.Requires(durability != null);
            return !durability.IsIndestructible() && durability.Current == 0;
        }
        #endregion
    }
}
