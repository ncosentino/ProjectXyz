using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IDropLink : IHasDropWeighting
    {
        #region Properties
        Guid Id { get; }

        Guid ParentDropId { get; }

        Guid ChildDropId { get; }
        #endregion
    }
}
