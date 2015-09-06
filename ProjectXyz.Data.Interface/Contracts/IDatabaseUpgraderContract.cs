using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Data.Interface.Contracts
{
    [ContractClassFor(typeof(IDatabaseUpgrader))]
    public abstract class IDatabaseUpgraderContract : IDatabaseUpgrader
    {
        #region Properties
        public int CurrentSchemaVersion
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 1);
                return default(int);
            }
        }
        #endregion

        #region Methods
        public void UpgradeDatabase(IDatabase database, int sourceSchemaVersion, int destinationSchemaVersion)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentException>(sourceSchemaVersion < destinationSchemaVersion);
        }
        #endregion
    }
}
