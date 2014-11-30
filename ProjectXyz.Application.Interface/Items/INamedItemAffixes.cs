using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Items
{
    public interface INamedItemAffixes
    {
        #region Properties
        INamedItemAffix Prefix { get; }

        INamedItemAffix Suffix { get; }
        #endregion
    }
}
