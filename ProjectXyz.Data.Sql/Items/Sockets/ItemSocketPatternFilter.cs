using System;
using System.Collections.Generic;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Sql.Items.Sockets
{
    public sealed class ItemSocketPatternFilter : IItemSocketPatternFilter
    {
        #region Methods
        public IEnumerable<ISocketPatternDefinition> Filter(
            Guid sourceItemType,
            Guid sourceItemMagicType,
            IEnumerable<Guid> socketItemDefinitionIds,
            Guid weatherTypeId,
            Guid timeOfDayId)
        {
            yield break;
        }
        #endregion
    }
}