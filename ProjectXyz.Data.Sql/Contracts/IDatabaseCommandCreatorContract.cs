using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Sql.Contracts
{
    [ContractClassFor(typeof(IDatabaseCommandCreator))]
    public abstract class IDatabaseCommandCreatorContract : IDatabaseCommandCreator
    {
        #region Methods
        public IDbCommand CreateCommand(string commandText)
        {
            Contract.Requires<ArgumentNullException>(commandText != null);
            Contract.Requires<ArgumentException>(commandText != string.Empty);
            Contract.Ensures(Contract.Result<IDbCommand>() != null);

            return default(IDbCommand);
        }

        public IDbCommand CreateCommand(string commandText, string parameterName, object parameterValue)
        {
            Contract.Requires<ArgumentNullException>(commandText != null);
            Contract.Requires<ArgumentException>(commandText != string.Empty);
            Contract.Requires<ArgumentNullException>(parameterName != null);
            Contract.Requires<ArgumentException>(parameterName != string.Empty);
            Contract.Ensures(Contract.Result<IDbCommand>() != null);

            return default(IDbCommand);
        }

        public IDbCommand CreateCommand(string commandText, IDictionary<string, object> namedParameters)
        {
            Contract.Requires<ArgumentNullException>(commandText != null);
            Contract.Requires<ArgumentException>(commandText != string.Empty);
            Contract.Requires<ArgumentNullException>(namedParameters != null);
            Contract.Ensures(Contract.Result<IDbCommand>() != null);

            return default(IDbCommand);
        }

        public IDbDataParameter CreateParameter(IDbCommand command, string name, object value)
        {
            Contract.Requires<ArgumentNullException>(command != null);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);
            Contract.Ensures(Contract.Result<IDbDataParameter>() != null);

            return default(IDbDataParameter);
        }
        #endregion
    }
}
