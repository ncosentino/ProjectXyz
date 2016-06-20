using System;
using System.Data;
using System.Globalization;

namespace ClassLibrary1.Plugins.Enchantments.Expressions
{
    public sealed class DataTableExpressionEvaluator : IStringExpressionEvaluator
    {
        #region Fields
        private readonly DataTable _dataTable;
        #endregion

        #region Constructors
        public DataTableExpressionEvaluator()
        {
            _dataTable = new DataTable();
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
            catch (SyntaxErrorException ex)
            {
                throw new FormatException(string.Format("The expression '{0}' was in an invalid format and could not be evaulated.", expression), ex);
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
