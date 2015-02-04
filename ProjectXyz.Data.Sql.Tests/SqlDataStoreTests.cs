using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

using Moq;

using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Data.Sql.Tests
{
    [DataLayer]
    public class SqlDataStoreTests
    {
        #region Methods
        [Fact]
        public void SqlDataStore_OpenDatabase_ChecksVersion()
        {
            var userVersionReader = MockUserVersionReader.ForVersion(0);

            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(userVersionReader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.Query("PRAGMA user_version"))
                .Returns(userVersionReader.Object);

            var upgrader = new Mock<IDatabaseUpgrader>();
            upgrader
                .Setup(x => x.CurrentSchemaVersion)
                .Returns(1);

            var dataStore = SqlDataStore.Create(
                database.Object,
                upgrader.Object);

            userVersionReader.Verify(x => x.Read(), Times.Once());
            userVersionReader.Verify(x => x.GetOrdinal("user_version"), Times.Once());
            userVersionReader.Verify(x => x.GetInt32(0), Times.Once());
        }

        [Fact]
        public void SqlDataStore_OpenDatabaseNoRead_Throws()
        {
            var userVersionReader = new Mock<IDataReader>();
            userVersionReader
                .Setup(x => x.Read())
                .Returns(false);

            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(userVersionReader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.Query("PRAGMA user_version"))
                .Returns(userVersionReader.Object);

            var upgrader = new Mock<IDatabaseUpgrader>();
            upgrader
                .Setup(x => x.CurrentSchemaVersion)
                .Returns(1);

            var exception = Assert.Throws<InvalidOperationException>(() => SqlDataStore.Create(
                database.Object,
                upgrader.Object));
            Assert.Equal(
                "Could not read the current schema version.",
                exception.Message);
        }

        [Fact]
        public void SqlDataStore_OpenDatabaseFutureVersion_Throws()
        {
            var userVersionReader = MockUserVersionReader.ForVersion(1);

            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(userVersionReader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.Query("PRAGMA user_version"))
                .Returns(userVersionReader.Object);

            var upgrader = new Mock<IDatabaseUpgrader>();
            upgrader
                .Setup(x => x.CurrentSchemaVersion)
                .Returns(0);

            var exception = Assert.Throws<InvalidOperationException>(() => SqlDataStore.Create(
                database.Object,
                upgrader.Object));
            Assert.Equal(
                "The schema version of the database (1) is later than the expected current version (0).",
                exception.Message);
        }

        [Fact]
        public void SqlDataStore_OpenDatabase_SetsDatabasePragmas()
        {
            var userVersionReader = MockUserVersionReader.ForVersion(0);

            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(userVersionReader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.Query("PRAGMA user_version"))
                .Returns(userVersionReader.Object);

            var upgrader = new Mock<IDatabaseUpgrader>();
            upgrader
                .Setup(x => x.CurrentSchemaVersion)
                .Returns(1);

            var dataStore = SqlDataStore.Create(
                database.Object,
                upgrader.Object);

            database.Verify(x => x.Execute("PRAGMA foreign_keys=1"), Times.Once());
            database.Verify(x => x.Execute("PRAGMA journal_mode=WAL"), Times.Once());
        }

        [Fact]
        public void SqlDataStore_OpenNewDatabase_UpgradesToVersion1()
        {
            var userVersionReader = MockUserVersionReader.ForVersion(0);

            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(userVersionReader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.Query("PRAGMA user_version"))
                .Returns(userVersionReader.Object);

            var upgrader = new Mock<IDatabaseUpgrader>();
            upgrader
                .Setup(x => x.CurrentSchemaVersion)
                .Returns(1);

            var dataStore = SqlDataStore.Create(
                database.Object,
                upgrader.Object);

            upgrader.Verify(x => x.CurrentSchemaVersion, Times.AtLeastOnce());
            upgrader.Verify(x => x.UpgradeDatabase(database.Object, 0, 1), Times.Once());
        }

        [Fact]
        public void SqlDataStore_OpenExistingDatabase_NoUpgrade()
        {
            var userVersionReader = MockUserVersionReader.ForVersion(1);

            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(userVersionReader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.Query("PRAGMA user_version"))
                .Returns(userVersionReader.Object);

            var upgrader = new Mock<IDatabaseUpgrader>();
            upgrader
                .Setup(x => x.CurrentSchemaVersion)
                .Returns(1);

            var dataStore = SqlDataStore.Create(
                database.Object,
                upgrader.Object);
            
            database.Verify(x => x.Execute("PRAGMA foreign_keys=1"), Times.Once());
            database.Verify(x => x.Execute("PRAGMA journal_mode=WAL"), Times.Once());

            upgrader.Verify(x => x.CurrentSchemaVersion, Times.AtLeastOnce());
            upgrader.Verify(x => x.UpgradeDatabase(database.Object, 0, 1), Times.Never);
        }
        #endregion

        #region Classes
        private static class MockUserVersionReader
        {
            public static Mock<IDataReader> ForVersion(int version)
            {
                var userVersionReader = new Mock<IDataReader>();
                userVersionReader
                    .Setup(x => x.Read())
                    .Returns(true);
                userVersionReader
                    .Setup(x => x.GetOrdinal("user_version"))
                    .Returns(0);
                userVersionReader
                    .Setup(x => x.GetInt32(0))
                    .Returns(version);

                return userVersionReader;
            }
        }
        #endregion
    }
}