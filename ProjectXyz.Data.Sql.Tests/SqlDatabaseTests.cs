using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using Moq;

using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Data.Sql.Tests
{
    [DataLayer]
    public class SqlDatabaseTests
    {
        #region Methods
        [Fact]
        public void SqlDatabase_UsingBlockWithoutOwnership_DoesNotCloseConnection()
        {
            var connection = new Mock<IDbConnection>();

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
            }

            connection.Verify(x => x.Close(), Times.Never);
            connection.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public void SqlDatabase_UsingBlockWithOwnership_ClosesConnection()
        {
            var connection = new Mock<IDbConnection>();

            using (var database = SqlDatabase.Create(connection.Object, true))
            {
            }

            connection.Verify(x => x.Close(), Times.Once());
            connection.Verify(x => x.Dispose(), Times.Once());
        }

        [Fact]
        public void SqlDatabase_Open_OpensConnection()
        {
            var connection = new Mock<IDbConnection>();

            using (var database = SqlDatabase.Create(connection.Object, true))
            {
                database.Open();
            }

            connection.Verify(x => x.Open(), Times.Once());
        }

        [Fact]
        public void SqlDatabase_CloseExistingConnection_ClosesConnection()
        {
            var connection = new Mock<IDbConnection>();

            using (var database = SqlDatabase.Create(connection.Object, true))
            {
                database.Open();
                database.Close();
            }

            connection.Verify(x => x.Close(), Times.Exactly(2));
        }
        #endregion
    }
}
