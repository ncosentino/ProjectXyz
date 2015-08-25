using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class DataTableExpressionEvaluator : IStringExpressionEvaluator
    {
        #region Fields
        private readonly DataTable _dataTable;
        #endregion

        #region Constructors
        private DataTableExpressionEvaluator()
        {
            _dataTable = new DataTable();
        }

        ~DataTableExpressionEvaluator()
        {
            Dispose(false);
        }
        #endregion

        #region Methods
        public static IStringExpressionEvaluator Create()
        {
            var stringExpressionEvaluator = new DataTableExpressionEvaluator();
            return stringExpressionEvaluator;
        }

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

            return Convert.ToDouble(value, CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataTable.Dispose();
            }
        }
        #endregion
    }
}
