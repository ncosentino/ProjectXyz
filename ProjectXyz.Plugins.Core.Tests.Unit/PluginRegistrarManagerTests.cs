using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface;
using Xunit;

namespace ProjectXyz.Plugins.Core.Tests.Unit
{
    public class PluginRegistrarManagerTests
    {
        #region Methods
        [Fact]
        private void Create_ValidArguments_NotNull()
        {
            // Setup
            var dataManager = new Mock<IDataManager>(MockBehavior.Strict);

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>(MockBehavior.Strict);

            var enchantmentSaver = new Mock<IEnchantmentSaver>(MockBehavior.Strict);

            var enchantmentFactory = new Mock<IEnchantmentFactory>(MockBehavior.Strict);

            var enchantmentApplicationFactoryManager = new Mock<IEnchantmentApplicationFactoryManager>(MockBehavior.Strict);
            enchantmentApplicationFactoryManager
                .Setup(x => x.Enchantments)
                .Returns(enchantmentFactory.Object);

            var enchantmentApplicationManager = new Mock<IEnchantmentApplicationManager>(MockBehavior.Strict);
            enchantmentApplicationManager
                .Setup(x => x.EnchantmentGenerator)
                .Returns(enchantmentGenerator.Object);
            enchantmentApplicationManager
                .Setup(x => x.EnchantmentSaver)
                .Returns(enchantmentSaver.Object);
            enchantmentApplicationManager
                .Setup(x => x.Factories)
                .Returns(enchantmentApplicationFactoryManager.Object);

            var applicationManager = new Mock<IApplicationManager>(MockBehavior.Strict);
            applicationManager
                .Setup(x => x.Enchantments)
                .Returns(enchantmentApplicationManager.Object);

            // Execute
            var result = PluginRegistrarManager.Create(
                dataManager.Object,
                applicationManager.Object);

            // Assert
            Assert.NotNull(result);

            enchantmentApplicationFactoryManager.Verify(x => x.Enchantments, Times.Once);

            enchantmentApplicationManager.Verify(x => x.EnchantmentGenerator, Times.Once);
            enchantmentApplicationManager.Verify(x => x.EnchantmentSaver, Times.Once);
            enchantmentApplicationManager.Verify(x => x.Factories, Times.Once);

            applicationManager.Verify(x => x.Enchantments, Times.AtLeastOnce());
        }
        #endregion
    }
}
