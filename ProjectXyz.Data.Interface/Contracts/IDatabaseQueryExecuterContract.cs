using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ProjectXyz.Data.Interface.Contracts
{
    [ContractClassFor(typeof(IDatabaseQueryExecutor))]
    public abstract class IDatabaseQueryExecutorContract : IDatabaseQueryExecutor
    {
        #region Methods
        public IDataReader Query(string queryText)
        {
            Contract.Requires<ArgumentNullException>(queryText != null);
            Contract.Requires<ArgumentException>(queryText != string.Empty);
            Contract.Ensures(Contract.Result<IDataReader>() != null);

            return default(IDataReader);
        }

        public IDataReader Query(string queryText, string parameterName, object parameterValue)
        {
            Contract.Requires<ArgumentNullException>(queryText != null);
            Contract.Requires<ArgumentException>(queryText != string.Empty);
            Contract.Requires<ArgumentNullException>(parameterName != null);
            Contract.Requires<ArgumentException>(parameterName != string.Empty);
            Contract.Ensures(Contract.Result<IDataReader>() != null);

            return default(IDataReader);
        }

        public IDataReader Query(string queryText, IDictionary<string, object> namedParameters)
        {
            Contract.Requires<ArgumentNullException>(queryText != null);
            Contract.Requires<ArgumentException>(queryText != string.Empty);
            Contract.Requires<ArgumentNullException>(namedParameters != null);
            Contract.Ensures(Contract.Result<IDataReader>() != null);

            return default(IDataReader);
        }
        #endregion
    }
}
