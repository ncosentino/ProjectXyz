using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Sql.Contracts;

namespace ProjectXyz.Data.Sql
{
    [ContractClass(typeof(IDatabaseContract))]
    public interface IDatabase : IDatabaseCommandCreator, IDatabaseCommandExecuter, IDatabaseQueryExecutor, IDisposable
    {
        #region Methods
        void Open();

        void Close();
        #endregion
    }
}
