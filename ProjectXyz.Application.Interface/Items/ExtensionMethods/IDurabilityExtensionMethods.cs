using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class IDurabilityExtensionMethods
    {
        #region Methods
        public static bool IsIndestructible(this IDurable durability)
        {
            Contract.Requires(durability != null);
            return durability.MaximumDurability == 0;
        }

        public static bool IsBroken(this IDurable durability)
        {
            Contract.Requires(durability != null);
            return !durability.IsIndestructible() && durability.CurrentDurability == 0;
        }
        #endregion
    }
}
