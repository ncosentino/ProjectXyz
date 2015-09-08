using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IDrop
    {
        #region Properties
        Guid Id { get; }

        int Minimum { get; }

        int Maximum { get; }

        bool CanRepeat { get; }
        #endregion
    }
}
