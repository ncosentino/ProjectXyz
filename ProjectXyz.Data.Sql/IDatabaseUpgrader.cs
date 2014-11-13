using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Sql
{
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
