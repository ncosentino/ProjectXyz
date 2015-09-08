using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IDropLinkFactory
    {
        #region Methods
        IDropLink Create(
            Guid id,
            Guid parentDropId,
            Guid childDropId,
            int weighting);
        #endregion
    }
}
