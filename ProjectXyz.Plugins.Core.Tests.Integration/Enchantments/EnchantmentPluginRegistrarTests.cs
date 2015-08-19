using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;
using ProjectXyz.Plugins.Core.Enchantments;
using ProjectXyz.Plugins.Enchantments;
using Xunit;

namespace ProjectXyz.Plugins.Core.Tests.Integration.Enchantments
{
    public class EnchantmentPluginRegistrarTests
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

            var enchantmentDefinitionRepository = new Mock<IEnchantmentDefinitionRepository>(MockBehavior.Strict);

            var enchantmentWeatherRepository = new Mock<IEnchantmentWeatherRepository>(MockBehavior.Strict);

            var plugin = new Plugins.Enchantments.Additive.Plugin(
                database.Object,
                enchantmentDefinitionRepository.Object,
                enchantmentWeatherRepository.Object);

            enchantmentSaver
                .Setup(x => x.RegisterCallbackForType(typeof(Plugins.Enchantments.Additive.IAdditiveEnchantment), plugin.SaveEnchantmentCallback));
            enchantmentGenerator
                .Setup(x => x.RegisterCallbackForType(typeof(Plugins.Enchantments.Additive.IAdditiveEnchantmentDefinition), plugin.GenerateEnchantmentCallback));
            enchantmentFactory
                .Setup(x => x.RegisterCallbackForType(typeof(Plugins.Enchantments.Additive.IAdditiveEnchantmentStore), plugin.CreateEnchantmentCallback));

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
