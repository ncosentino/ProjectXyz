using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentSaverTests
    {
        [Fact]
        public void SaveEnchantment_RegisteredType_Success()
        {
            // Setup
            var enchantmentStore = new Mock<IEnchantmentStore>();

            var enchantment = new Mock<IEnchantment>(MockBehavior.Strict);

            SaveEnchantmentDelegate callback = x =>
            {
                return enchantmentStore.Object;
            };

            var enchantmentSaver = EnchantmentSaver.Create();
            enchantmentSaver.RegisterCallbackForType(enchantment.Object.GetType(), callback);

            // Execute
            var result = enchantmentSaver.Save(enchantment.Object);

            // Assert
            Assert.Equal(enchantmentStore.Object, result);
        }

        [Fact]
        public void SaveEnchantment_TypeNotRegistered_ThrowsInvalidOperationException()
        {
            // Setup
            var enchantment = new Mock<IEnchantment>(MockBehavior.Strict);

            var enchantmentSaver = EnchantmentSaver.Create();

            Assert.ThrowsDelegateWithReturn method = () => enchantmentSaver.Save(enchantment.Object);

            // Execute
            var result = Assert.Throws<InvalidOperationException>(method);

            // Assert
        }
    }
}