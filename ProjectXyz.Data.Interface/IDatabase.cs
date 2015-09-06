using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Contracts;

namespace ProjectXyz.Data.Interface
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
