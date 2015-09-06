using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Contracts;

namespace ProjectXyz.Data.Interface
{
    [ContractClass(typeof(IDatabaseCommandCreatorContract))]
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
