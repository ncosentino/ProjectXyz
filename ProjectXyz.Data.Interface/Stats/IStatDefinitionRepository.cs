using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStatDefinitionRepository
    {
        #region Methods
        IStatDefinition Add(
            Guid id,
            Guid nameStringResourceId);

        void RemoveById(Guid id);

        IStatDefinition GetById(Guid id);

        IEnumerable<IStatDefinition> GetAll();
        #endregion
    }
}
