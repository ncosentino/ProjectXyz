using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface IItemSocketPatternFilter
    {
        IEnumerable<ISocketPatternDefinition> Filter(
            Guid sourceItemType,
            Guid sourceItemMagicType,
            IEnumerable<Guid> socketItemDefinitionIds,
            Guid weatherTypeId,
            Guid timeOfDayId);
    }
}