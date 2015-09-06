using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Contracts;

namespace ProjectXyz.Data.Interface
{
    [ContractClass(typeof(IDatabaseUpgraderContract))]
    public interface IDatabaseUpgrader
    {
        #region Properties
        int CurrentSchemaVersion { get; }
        #endregion

        #region Methods
        void UpgradeDatabase(IDatabase database, int sourceSchemaVersion, int destinationSchemaVersion);
        #endregion
    }
}
