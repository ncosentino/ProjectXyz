using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Sql.Contracts
{
    [ContractClassFor(typeof(IDatabase))]
    public abstract class IDatabaseContract : IDatabase, IDatabaseCommandCreator, IDatabaseCommandExecuter, IDatabaseQueryExecutor, IDisposable
    {
        #region Methods
        public void Open()
        {
        }

        public void Close()
        {
        }

        public abstract void Dispose();

        public abstract IDataReader Query(string queryText);

        public abstract IDataReader Query(string queryText, string parameterName, object parameterValue);

        public abstract IDataReader Query(string queryText, IDictionary<string, object> namedParameters);

        public abstract int Execute(string commandText);

        public abstract int Execute(string commandText, string parameterName, object parameterValue);

        public abstract int Execute(string commandText, IDictionary<string, object> namedParameters);

        public abstract IDbCommand CreateCommand(string commandText);

        public abstract IDbCommand CreateCommand(string commandText, string parameterName, object parameterValue);

        public abstract IDbCommand CreateCommand(string commandText, IDictionary<string, object> namedParameters);

        public abstract IDbDataParameter CreateParameter(IDbCommand command, string name, object value);
        #endregion
    }
}
