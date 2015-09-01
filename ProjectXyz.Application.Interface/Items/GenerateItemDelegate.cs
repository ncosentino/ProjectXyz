using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items
{
    public delegate IItem GenerateItemDelegate(
        IRandom randomizer,
        Guid itemDefinitionId,
        int level,
        IItemContext itemContext);
}
