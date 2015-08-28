using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface IStatSocketTypeRepository
    {
        #region Methods
        void Add(IStatSocketType statSocketType);

        void RemoveById(Guid id);

        IStatSocketType GetByStatDefinitionId(Guid statDefinitionId);

        IStatSocketType GetBySocketTypeId(Guid socketTypeId);

        IEnumerable<IStatSocketType> GetAll();
        #endregion
    }
}
