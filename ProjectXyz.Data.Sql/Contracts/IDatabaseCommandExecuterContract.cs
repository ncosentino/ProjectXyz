using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Sql.Contracts
{
    [ContractClassFor(typeof(IDatabaseCommandExecuter))]
    public abstract class IDatabaseCommandExecuterContract : IDatabaseCommandExecuter
    {
        #region Methods
        public int Execute(string commandText)
        {
            Contract.Requires<ArgumentNullException>(commandText != null);
            Contract.Requires<ArgumentException>(commandText != string.Empty);
            Contract.Ensures(Contract.Result<int>() >= 0);

            return default(int);
        }

        public int Execute(string commandText, string parameterName, object parameterValue)
        {
            Contract.Requires<ArgumentNullException>(commandText != null);
            Contract.Requires<ArgumentException>(commandText != string.Empty);
            Contract.Requires<ArgumentNullException>(parameterName != null);
            Contract.Requires<ArgumentException>(parameterName != string.Empty);
            Contract.Ensures(Contract.Result<int>() >= 0);

            return default(int);
        }

        public int Execute(string commandText, IDictionary<string, object> namedParameters)
        {
            Contract.Requires<ArgumentNullException>(commandText != null);
            Contract.Requires<ArgumentException>(commandText != string.Empty);
            Contract.Requires<ArgumentNullException>(namedParameters != null);
            Contract.Ensures(Contract.Result<int>() >= 0);

            return default(int);
        }
        #endregion
    }
}
