using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStatDefinitionFactory
    {
        #region Methods
        IStatDefinition Create(
            Guid id,
            Guid nameStringResourceId);
        #endregion
    }
}
