using System;

using Xunit;

using Moq;

using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Data.Sql.Tests
{
    [DataLayer]
    public class SqlDatabaseUpgraderTests
    {
        #region Methods
        [Fact]
        public void SqlDatabaseUpgrader_Upgrade0To1_CreatesSchema()
        {
            var database = new Mock<IDatabase>();
            var upgrader = SqlDatabaseUpgrader.Create();

            upgrader.UpgradeDatabase(
                database.Object,
                0,
                1);

            database.Verify(x => x.Execute("PRAGMA user_version=1"), Times.Once());
            database.Verify(x => x.Execute(It.IsRegex("CREATE TABLE.*", System.Text.RegularExpressions.RegexOptions.IgnoreCase)), Times.Once());
        }

        [Fact]
        public void SqlDatabaseUpgrader_UpgradePastCurrent_Throws()
        {
            var database = new Mock<IDatabase>();
            var upgrader = SqlDatabaseUpgrader.Create();

            var exception = Assert.Throws<NotSupportedException>(() => 
                upgrader.UpgradeDatabase(
                    database.Object,
                    0,
                    upgrader.CurrentSchemaVersion + 1));
            Assert.Equal(
                "Cannot upgrade from schema version 0 to version 2.",
                exception.Message);
        }
        #endregion
    }
}