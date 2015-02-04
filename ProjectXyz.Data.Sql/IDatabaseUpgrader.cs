using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Sql.Contracts;

namespace ProjectXyz.Data.Sql
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
