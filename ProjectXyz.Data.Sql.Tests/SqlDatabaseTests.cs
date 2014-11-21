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

        [Fact]
        public void SqlDatabase_CreateCommandNoParameters_CreatesParameterlessCommand()
        {
            Mock<IDbCommand> command;
            Mock<IDbConnection> connection;
            SetupNoParameters(out command, out connection);

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
                database.CreateCommand("This is some SQL");
            }

            command.VerifySet(x => x.CommandText = "This is some SQL", Times.Once());
            command.VerifyGet(x => x.Parameters, Times.Never);
        }

        [Fact]
        public void SqlDatabase_CreateCommandSingleParameter_CreatesCommandWithParameter()
        {
            Mock<IDataParameterCollection> parameters;
            Mock<IDbDataParameter> parameter;
            Mock<IDbCommand> command;
            Mock<IDbConnection> connection;
            SetupSingleParameter(out parameters, out parameter, out command, out connection);

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
                database.CreateCommand("This is some SQL", "Parameter Name", "Value");
            }

            command.VerifySet(x => x.CommandText = "This is some SQL", Times.Once());
            parameters.Verify(x => x.Add(It.IsAny<object>()), Times.Once());
            parameter.VerifySet(x => x.ParameterName = "Parameter Name", Times.Once());
            parameter.VerifySet(x => x.Value = "Value", Times.Once());
        }

        [Fact]
        public void SqlDatabase_CreateCommandMultiParameter_CreatesCommandWithParameters()
        {
            Mock<IDataParameterCollection> parameters;
            Mock<IDbDataParameter> parameter1;
            Mock<IDbDataParameter> parameter2;
            Mock<IDbCommand> command;
            Mock<IDbConnection> connection;
            SetupMultiParameter(out parameters, out parameter1, out parameter2, out command, out connection);

            var parametersToSet = new Dictionary<string, object>()
            {
                { "Parameter 1", 1},
                { "Parameter 2", 2},
            };

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
                database.CreateCommand("This is some SQL", parametersToSet);
            }

            command.VerifySet(x => x.CommandText = "This is some SQL", Times.Once());
            parameters.Verify(x => x.Add(It.IsAny<object>()), Times.Exactly(2));
            parameter1.VerifySet(x => x.ParameterName = "Parameter 1", Times.Once());
            parameter1.VerifySet(x => x.Value = 1, Times.Once());
            parameter2.VerifySet(x => x.ParameterName = "Parameter 2", Times.Once());
            parameter2.VerifySet(x => x.Value = 2, Times.Once());
        }

        [Fact]
        public void SqlDatabase_ExecuteNoParameters_ExecutesParameterlessCommand()
        {
            Mock<IDbCommand> command;
            Mock<IDbConnection> connection;
            SetupNoParameters(out command, out connection);

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
                database.Execute("This is some SQL");
            }

            command.VerifySet(x => x.CommandText = "This is some SQL", Times.Once());
            command.VerifyGet(x => x.Parameters, Times.Never);
            command.Verify(x => x.ExecuteNonQuery(), Times.Once());
        }

        [Fact]
        public void SqlDatabase_ExecuteSingleParameter_ExecutesCommandWithParameter()
        {
            Mock<IDataParameterCollection> parameters;
            Mock<IDbDataParameter> parameter;
            Mock<IDbCommand> command;
            Mock<IDbConnection> connection;
            SetupSingleParameter(out parameters, out parameter, out command, out connection);

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
                database.Execute("This is some SQL", "Parameter Name", "Value");
            }

            command.VerifySet(x => x.CommandText = "This is some SQL", Times.Once());
            parameters.Verify(x => x.Add(It.IsAny<object>()), Times.Once());
            parameter.VerifySet(x => x.ParameterName = "Parameter Name", Times.Once());
            parameter.VerifySet(x => x.Value = "Value", Times.Once());
            command.Verify(x => x.ExecuteNonQuery(), Times.Once());
        }

        [Fact]
        public void SqlDatabase_ExecuteMultiParameter_ExecuteCommandWithParameters()
        {
            Mock<IDataParameterCollection> parameters;
            Mock<IDbDataParameter> parameter1;
            Mock<IDbDataParameter> parameter2;
            Mock<IDbCommand> command;
            Mock<IDbConnection> connection;
            SetupMultiParameter(out parameters, out parameter1, out parameter2, out command, out connection);

            var parametersToSet = new Dictionary<string, object>()
            {
                { "Parameter 1", 1},
                { "Parameter 2", 2},
            };

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
                database.Execute("This is some SQL", parametersToSet);
            }

            command.VerifySet(x => x.CommandText = "This is some SQL", Times.Once());
            parameters.Verify(x => x.Add(It.IsAny<object>()), Times.Exactly(2));
            parameter1.VerifySet(x => x.ParameterName = "Parameter 1", Times.Once());
            parameter1.VerifySet(x => x.Value = 1, Times.Once());
            parameter2.VerifySet(x => x.ParameterName = "Parameter 2", Times.Once());
            parameter2.VerifySet(x => x.Value = 2, Times.Once());
            command.Verify(x => x.ExecuteNonQuery(), Times.Once());
        }

        [Fact]
        public void SqlDatabase_QueryNoParameters_ExecutesParameterlessQuery()
        {
            Mock<IDbCommand> command;
            Mock<IDbConnection> connection;
            SetupNoParameters(out command, out connection);

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
                database.Query("This is some SQL");
            }

            command.VerifySet(x => x.CommandText = "This is some SQL", Times.Once());
            command.VerifyGet(x => x.Parameters, Times.Never);
            command.Verify(x => x.ExecuteReader(), Times.Once());
        }

        [Fact]
        public void SqlDatabase_QuerySingleParameter_ExecutesQueryWithParameter()
        {
            Mock<IDataParameterCollection> parameters;
            Mock<IDbDataParameter> parameter;
            Mock<IDbCommand> command;
            Mock<IDbConnection> connection;
            SetupSingleParameter(out parameters, out parameter, out command, out connection);

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
                database.Query("This is some SQL", "Parameter Name", "Value");
            }

            command.VerifySet(x => x.CommandText = "This is some SQL", Times.Once());
            parameters.Verify(x => x.Add(It.IsAny<object>()), Times.Once());
            parameter.VerifySet(x => x.ParameterName = "Parameter Name", Times.Once());
            parameter.VerifySet(x => x.Value = "Value", Times.Once());
            command.Verify(x => x.ExecuteReader(), Times.Once());
        }

        [Fact]
        public void SqlDatabase_QueryMultiParameter_ExecuteQueryWithParameters()
        {
            Mock<IDataParameterCollection> parameters;
            Mock<IDbDataParameter> parameter1;
            Mock<IDbDataParameter> parameter2;
            Mock<IDbCommand> command;
            Mock<IDbConnection> connection;
            SetupMultiParameter(out parameters, out parameter1, out parameter2, out command, out connection);

            var parametersToSet = new Dictionary<string, object>()
            {
                { "Parameter 1", 1},
                { "Parameter 2", 2},
            };

            using (var database = SqlDatabase.Create(connection.Object, false))
            {
                database.Query("This is some SQL", parametersToSet);
            }

            command.VerifySet(x => x.CommandText = "This is some SQL", Times.Once());
            parameters.Verify(x => x.Add(It.IsAny<object>()), Times.Exactly(2));
            parameter1.VerifySet(x => x.ParameterName = "Parameter 1", Times.Once());
            parameter1.VerifySet(x => x.Value = 1, Times.Once());
            parameter2.VerifySet(x => x.ParameterName = "Parameter 2", Times.Once());
            parameter2.VerifySet(x => x.Value = 2, Times.Once());
            command.Verify(x => x.ExecuteReader(), Times.Once());
        }

        private static void SetupMultiParameter(out Mock<IDataParameterCollection> parameters, out Mock<IDbDataParameter> parameter1, out Mock<IDbDataParameter> parameter2, out Mock<IDbCommand> command, out Mock<IDbConnection> connection)
        {
            parameters = new Mock<IDataParameterCollection>();
            parameter1 = new Mock<IDbDataParameter>();
            parameter2 = new Mock<IDbDataParameter>();

            command = new Mock<IDbCommand>();
            command
                .Setup(x => x.Parameters)
                .Returns(parameters.Object);
            command
                .SetupSequence(x => x.CreateParameter())
                .Returns(parameter1.Object)
                .Returns(parameter2.Object);

            connection = new Mock<IDbConnection>();
            connection
                .Setup(x => x.CreateCommand())
                .Returns(command.Object);
        }

        private static void SetupSingleParameter(out Mock<IDataParameterCollection> parameters, out Mock<IDbDataParameter> parameter, out Mock<IDbCommand> command, out Mock<IDbConnection> connection)
        {
            parameters = new Mock<IDataParameterCollection>();
            parameter = new Mock<IDbDataParameter>();

            command = new Mock<IDbCommand>();
            command
                .Setup(x => x.Parameters)
                .Returns(parameters.Object);
            command
                .Setup(x => x.CreateParameter())
                .Returns(parameter.Object);

            connection = new Mock<IDbConnection>();
            connection
                .Setup(x => x.CreateCommand())
                .Returns(command.Object);
        }

        private static void SetupNoParameters(out Mock<IDbCommand> command, out Mock<IDbConnection> connection)
        {
            command = new Mock<IDbCommand>();

            connection = new Mock<IDbConnection>();
            connection
                .Setup(x => x.CreateCommand())
                .Returns(command.Object);
        }
        #endregion
    }
}
