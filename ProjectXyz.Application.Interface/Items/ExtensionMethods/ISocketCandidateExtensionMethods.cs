using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Interface.Items.ExtensionMethods
{
    public static class ISocketCandidateExtensionMethods
    {
        #region Methods
        public static bool CanBeUsedForSocketing(this ISocketCandidate candidate)
        {
            Contract.Requires<ArgumentNullException>(candidate != null);
            return candidate.RequiredSockets > 0;
        }
        #endregion
    }
}
