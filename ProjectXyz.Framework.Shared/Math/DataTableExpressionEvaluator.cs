using System;
using System.Data;
using System.Globalization;
using ProjectXyz.Framework.Interface.Math;

namespace ProjectXyz.Framework.Shared.Math
{
    public sealed class DataTableExpressionEvaluator : IStringExpressionEvaluator
    {
        #region Fields
        private readonly DataTable _dataTable;
        #endregion

        #region Constructors
        public DataTableExpressionEvaluator(DataTable dataTable)
        {
            _dataTable = dataTable;
        }
        #endregion

        #region Methods
        public double Evaluate(string expression)
        {
            object value;

            try
            {
                value = _dataTable.Compute(expression, string.Empty);
            }
            catch (Exception ex) when (ex is EvaluateException || ex is SyntaxErrorException)
            {
                throw new FormatException($"The expression '{expression}' was in an invalid format and could not be evaluated.", ex);
            }

            return Convert.ToDouble(
                value,
                CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            if (_dataTable != null)
            {
                _dataTable.Dispose();
            }
        }
        #endregion
    }
}
