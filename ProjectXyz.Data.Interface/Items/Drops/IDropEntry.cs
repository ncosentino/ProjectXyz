using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IDropEntry : IHasDropWeighting
    {
        #region Properties
        Guid Id { get; }

        Guid ParentDropId { get; }

        Guid MagicTypeGroupingId { get; }

        Guid WeatherTypeGroupingId { get; }

        Guid ItemDefinitionId { get; }
        #endregion
    }
}
