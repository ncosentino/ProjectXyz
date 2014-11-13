using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Sql
{
    public interface IDatabaseCommandExecuter
    {
        #region Methods
        int Execute(string commandText);

        int Execute(string commandText, string parameterName, object parameterValue);

        int Execute(string commandText, IDictionary<string, object> namedParameters);
        #endregion
    }
}
