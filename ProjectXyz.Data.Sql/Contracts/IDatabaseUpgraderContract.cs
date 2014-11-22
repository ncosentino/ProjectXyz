using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Sql.Contracts
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
