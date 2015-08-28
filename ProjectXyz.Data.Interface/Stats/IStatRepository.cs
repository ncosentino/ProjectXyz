using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStatRepository
    {
        #region Methods
        IStat Add(
            Guid id,
            Guid statDefinitionId,
            double value);

        void RemoveById(Guid id);

        IStat GetById(Guid id);

        IEnumerable<IStat> GetAll();
        #endregion
    }
}
