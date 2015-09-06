using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Contracts;

namespace ProjectXyz.Data.Interface
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
