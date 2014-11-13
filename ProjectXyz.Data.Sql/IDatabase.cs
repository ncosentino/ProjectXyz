using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Sql
{
    public interface IDatabase : IDatabaseCommandCreator, IDatabaseCommandExecuter, IDatabaseQueryExecutor, IDisposable
    {
        #region Methods
        void Open();

        void Close();
        #endregion
    }
}
