using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items.Drops
{
    public interface IDropGenerator
    {
        #region Methods
        IEnumerable<IItem> Generate(
            IRandom randomizer,
            Guid dropId,
            int level,
            IItemContext itemContext);
        #endregion
    }
}
