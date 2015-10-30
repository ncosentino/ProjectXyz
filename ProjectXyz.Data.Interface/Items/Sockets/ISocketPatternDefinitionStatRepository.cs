using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketPatternDefinitionStatRepository
    {
        #region Methods
        ISocketPatternStat Add(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue);

        void RemoveById(Guid id);

        ISocketPatternStat GetById(Guid id);

        IEnumerable<ISocketPatternStat> GetAll();

        IEnumerable<ISocketPatternStat> GetBySocketPatternId(Guid socketPatternId);
        #endregion
    }
}