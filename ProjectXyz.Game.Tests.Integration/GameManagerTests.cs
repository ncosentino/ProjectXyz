using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Sql;
using ProjectXyz.Game.Core;
using ProjectXyz.Plugins.Items.Magic;
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
        public void EnumerateEnchantmentPlugins_ValidState_ExpectedPluginsLoaded()
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
        public void EnumerateItemPlugins_ValidState_ExpectedPluginsLoaded()
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

            var displayLanguage = gameManager.DataManager.Resources.DisplayLanguages.Add(Guid.NewGuid(), "Dummy Language");
            var magicStringResource = gameManager.DataManager.Resources.StringResources.Add(Guid.NewGuid(), displayLanguage.Id, "Magic Type");
            var magicMagicType = gameManager.DataManager.Items.MagicTypes.Add(
                Guid.NewGuid(),
                magicStringResource.Id);
            gameManager.DataManager.Items.ItemTypeGeneratorPlugins.Add(
                Guid.NewGuid(),
                magicMagicType.Id,
                typeof(MagicItemGenerator).FullName);

            // Execute
            var result = gameManager.PluginManager.Items.ToArray();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void EnumerateItemPlugins_MissingDtaabaseMappings_ThrowsException()
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
            Assert.ThrowsDelegateWithReturn method = () => gameManager.PluginManager.Items.ToArray();

            // Assert
            Assert.Throws<InvalidOperationException>(method);
        }
        #endregion
    }
}
