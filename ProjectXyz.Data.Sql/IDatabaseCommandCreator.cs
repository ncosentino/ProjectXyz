using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Sql
{
    public interface IDatabaseCommandCreator
    {
        #region Methods
        IDbCommand CreateCommand(string commandText);

        IDbCommand CreateCommand(string commandText, string parameterName, object parameterValue);

        IDbCommand CreateCommand(string commandText, IDictionary<string, object> namedParameters);

        IDbDataParameter CreateParameter(IDbCommand command, string name, object value);
        #endregion
    }
}
