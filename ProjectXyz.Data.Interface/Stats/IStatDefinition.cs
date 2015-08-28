using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Stats
{
    public interface IStatDefinition
    {
        #region Properties
        Guid Id { get; }

        Guid NameStringResourceId { get; }
        #endregion
    }
}
