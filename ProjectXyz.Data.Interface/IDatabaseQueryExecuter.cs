using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Contracts;

namespace ProjectXyz.Data.Interface
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
