using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketPatternStatFactory
    {
        #region Methods
        ISocketPatternStat Create(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue);
        #endregion
    }
}
