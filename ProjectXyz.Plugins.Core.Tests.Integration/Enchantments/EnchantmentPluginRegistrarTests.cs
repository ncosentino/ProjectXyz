using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Core.Enchantments;
using ProjectXyz.Plugins.Enchantments;
using ProjectXyz.Tests.Integration;
using Xunit;

namespace ProjectXyz.Plugins.Core.Tests.Integration.Enchantments
{
    public class EnchantmentPluginRegistrarTests : DatabaseTest
    {
        #region Methods
        [Fact]
        private void RegisterPlugins_AdditiveEnchantmentPlugin_Success()
        {
            // Setup
            var enchantmentFactory = new Mock<IEnchantmentFactory>(MockBehavior.Strict);

            var enchantmentSaver = new Mock<IEnchantmentSaver>(MockBehavior.Strict);

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>(MockBehavior.Strict);

            var database = new Mock<IDatabase>(MockBehavior.Strict);

            var dataManager = SqlDataManager.Create(
                Database,
                SqlDatabaseUpgrader.Create());

            var enchantmentApplicationFactoryManager = EnchantmentApplicationFactoryManager.Create();
                
            var plugin = new Plugins.Enchantments.Expression.Plugin(
                database.Object,
                dataManager,
                enchantmentApplicationFactoryManager);

            enchantmentSaver
                .Setup(x => x.RegisterCallbackForType(typeof(Plugins.Enchantments.Expression.IExpressionEnchantment), plugin.SaveEnchantmentCallback));
            enchantmentGenerator
                .Setup(x => x.RegisterCallbackForType(typeof(Plugins.Enchantments.Expression.IExpressionEnchantmentDefinition), plugin.GenerateEnchantmentCallback));
            enchantmentFactory
                .Setup(x => x.RegisterCallbackForType(typeof(Plugins.Enchantments.Expression.IExpressionEnchantmentStore), plugin.CreateEnchantmentCallback));

            var plugins = new IEnchantmentPlugin[]
            {
                plugin,
            };

            var enchantmentPluginRegistrar = EnchantmentPluginRegistrar.Create(
                enchantmentFactory.Object,
                enchantmentSaver.Object,
                enchantmentGenerator.Object);

            // Execute
            enchantmentPluginRegistrar.RegisterPlugins(plugins);

            // Assert
            enchantmentSaver.Verify(x => x.RegisterCallbackForType(It.IsAny<Type>(), It.IsAny<SaveEnchantmentDelegate>()), Times.Once);
            enchantmentGenerator.Verify(x => x.RegisterCallbackForType(It.IsAny<Type>(), It.IsAny<GenerateEnchantmentDelegate>()), Times.Once);
            enchantmentFactory.Verify(x => x.RegisterCallbackForType(It.IsAny<Type>(), It.IsAny<CreateEnchantmentDelegate>()), Times.Once);
        }
        #endregion
    }
}
