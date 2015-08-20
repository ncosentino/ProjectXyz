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
            Guid enchantmentId = Guid.NewGuid();
            Guid enchantmentTriggerId = Guid.NewGuid();
            Guid statusTypeId = Guid.NewGuid();
            Guid enchantmentTypeId = Guid.NewGuid();
            TimeSpan remainingDuration = TimeSpan.FromSeconds(123);
            Guid[] weatherIds = new[]
            {
                Guid.NewGuid(),
            };
            Guid enchantmentWeatherId = Guid.NewGuid();
            
            var enchantment = new Mock<IEnchantment>(MockBehavior.Strict);
            enchantment
                .Setup(x => x.Id)
                .Returns(enchantmentId);
            enchantment
                .Setup(x => x.WeatherIds)
                .Returns(weatherIds);
            enchantment
                .Setup(x => x.TriggerId)
                .Returns(enchantmentTriggerId);
            enchantment
                .Setup(x => x.StatusTypeId)
                .Returns(statusTypeId);
            enchantment
                .Setup(x => x.EnchantmentTypeId)
                .Returns(enchantmentTypeId);
            
            int saveEnchantmentDelegateCount = 0;
            SaveEnchantmentDelegate callback = x =>
            {
                Assert.Equal(enchantment.Object, x);
                saveEnchantmentDelegateCount++;
            };

            var enchantmentStore = new Mock<IEnchantmentStore>(MockBehavior.Strict);

            var enchantmentStoreFactory = new Mock<IEnchantmentStoreFactory>(MockBehavior.Strict);
            enchantmentStoreFactory
                .Setup(x => x.Create(enchantmentId, enchantmentTriggerId, statusTypeId, enchantmentTypeId, enchantmentWeatherId))
                .Returns(enchantmentStore.Object);

            var enchantmentStoreRepository = new Mock<IEnchantmentStoreRepository>(MockBehavior.Strict);
            enchantmentStoreRepository
                .Setup(x => x.Add(enchantmentStore.Object));
            
            var enchantmentWeather = new Mock<IEnchantmentWeather>(MockBehavior.Strict);
            enchantmentWeather
                .Setup(x => x.Id)
                .Returns(enchantmentWeatherId);

            var enchantmentWeatherFactory = new Mock<IEnchantmentWeatherFactory>(MockBehavior.Strict);
            enchantmentWeatherFactory
                .Setup(x => x.Create(It.IsAny<Guid>(), enchantmentId, weatherIds))
                .Returns(enchantmentWeather.Object);
            
            var enchantmentWeatherRepository = new Mock<IEnchantmentWeatherRepository>(MockBehavior.Strict);
            enchantmentWeatherRepository
                .Setup(x => x.Add(enchantmentWeather.Object));

            var enchantmentSaver = EnchantmentSaver.Create(
                enchantmentStoreFactory.Object,
                enchantmentStoreRepository.Object,
                enchantmentWeatherFactory.Object,
                enchantmentWeatherRepository.Object);
            enchantmentSaver.RegisterCallbackForType(enchantment.Object.GetType(), callback);

            // Execute
            enchantmentSaver.Save(enchantment.Object);

            // Assert
            Assert.Equal(1, saveEnchantmentDelegateCount);

            enchantment.Verify(x => x.Id, Times.AtLeastOnce());
            enchantment.Verify(x => x.WeatherIds, Times.Once);
            enchantment.Verify(x => x.TriggerId, Times.Once);
            enchantment.Verify(x => x.StatusTypeId, Times.Once);
            enchantment.Verify(x => x.EnchantmentTypeId, Times.Once);

            enchantmentStoreFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Once);

            enchantmentStoreRepository.Verify(x => x.Add(It.IsAny<IEnchantmentStore>()), Times.Once);

            enchantmentWeather.Verify(x => x.Id, Times.Once);

            enchantmentWeatherFactory.Verify(x => x.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<IEnumerable<Guid>>()), Times.Once);

            enchantmentWeatherRepository.Verify(x => x.Add(It.IsAny<IEnchantmentWeather>()), Times.Once);
        }

        [Fact]
        public void SaveEnchantment_TypeNotRegistered_ThrowsInvalidOperationException()
        {
            // Setup
            var enchantment = new Mock<IEnchantment>(MockBehavior.Strict);

            var enchantmentStoreFactory = new Mock<IEnchantmentStoreFactory>(MockBehavior.Strict);

            var enchantmentStoreRepository = new Mock<IEnchantmentStoreRepository>(MockBehavior.Strict);

            var enchantmentWeatherFactory = new Mock<IEnchantmentWeatherFactory>(MockBehavior.Strict);

            var enchantmentWeatherRepository = new Mock<IEnchantmentWeatherRepository>(MockBehavior.Strict);

            var enchantmentSaver = EnchantmentSaver.Create(
                enchantmentStoreFactory.Object,
                enchantmentStoreRepository.Object,
                enchantmentWeatherFactory.Object,
                enchantmentWeatherRepository.Object);

            Assert.ThrowsDelegateWithReturn method = () => enchantmentSaver.Save(enchantment.Object);

            // Execute
            var result = Assert.Throws<InvalidOperationException>(method);

            // Assert
        }
    }
}