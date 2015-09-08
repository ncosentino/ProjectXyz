using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IHasDropWeighting
    {
        #region Properties
        int Weighting { get; }
        #endregion
    }
}
