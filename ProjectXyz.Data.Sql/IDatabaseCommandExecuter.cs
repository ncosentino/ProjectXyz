using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Sql.Contracts;

namespace ProjectXyz.Data.Sql
{
    [ContractClass(typeof(IDatabaseCommandExecuterContract))]
    public interface IDatabaseCommandExecuter
    {
        #region Methods
        int Execute(string commandText);

        int Execute(string commandText, string parameterName, object parameterValue);

        int Execute(string commandText, IDictionary<string, object> namedParameters);
        #endregion
    }
}
