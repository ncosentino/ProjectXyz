using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Sql.Contracts;

namespace ProjectXyz.Data.Sql
{
    [ContractClass(typeof(IDatabaseQueryExecutorContract))]
    public interface IDatabaseQueryExecutor
    {
        #region Methods
        IDataReader Query(string queryText);

        IDataReader Query(string queryText, string parameterName, object parameterValue);

        IDataReader Query(string queryText, IDictionary<string, object> namedParameters);
        #endregion
    }
}
