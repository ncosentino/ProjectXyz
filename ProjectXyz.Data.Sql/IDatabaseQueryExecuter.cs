using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Sql
{
    public interface IDatabaseQueryExecutor
    {
        #region Methods
        IDataReader Query(string queryText);

        IDataReader Query(string queryText, string parameterName, object parameterValue);

        IDataReader Query(string queryText, IDictionary<string, object> namedParameters);
        #endregion
    }
}
