using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IDropFactory
    {
        #region Methods
        IDrop Create(
            Guid id,
            int minimum,
            int maximum,
            bool canRepeat);
        #endregion
    }
}
