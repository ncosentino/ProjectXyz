using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Sql;
using ProjectXyz.Game.Core;
using ProjectXyz.Tests.Integration;
using Xunit;

namespace ProjectXyz.Game.Tests.Integration
{
    public class GameManagerTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void Create_ValidArguments_NotNull()
        {
            // Setup
            var dataManager = SqlDataManager.Create(
                Database,
                SqlDatabaseUpgrader.Create());

            // Execute
            var result = GameManager.Create(
                Database,
                dataManager,
                new[]
                {
                    AppDomain.CurrentDomain.BaseDirectory,
                });

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreatedInstance_EnumerateEnchantmentPlugins_ExpectedPluginsLoaded()
        {
            // Setup
            var dataManager = SqlDataManager.Create(
                Database,
                SqlDatabaseUpgrader.Create());

            var gameManager = GameManager.Create(
                Database,
                dataManager,
                new[]
                {
                    AppDomain.CurrentDomain.BaseDirectory,
                });

            // Execute
            var result = gameManager.PluginManager.Enchantments.ToArray();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void CreatedInstance_EnumerateItemPlugins_ExpectedPluginsLoaded()
        {
            // Setup
            var dataManager = SqlDataManager.Create(
                Database,
                SqlDatabaseUpgrader.Create());

            var gameManager = GameManager.Create(
                Database,
                dataManager,
                new[]
                {
                    AppDomain.CurrentDomain.BaseDirectory,
                });

            // Execute
            var result = gameManager.PluginManager.Items.ToArray();

            // Assert
            Assert.Equal(2, result.Count());
        }
        #endregion
    }
}
