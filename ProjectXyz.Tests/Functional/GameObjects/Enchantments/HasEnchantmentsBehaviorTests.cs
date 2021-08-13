using System;
using System.Threading.Tasks;

using Autofac;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments;
using ProjectXyz.Tests.Functional.TestingData;

using Xunit;

namespace ProjectXyz.Tests.Functional.GameObjects.Enchantments
{
    public sealed class HasEnchantmentsBehaviorFunctionalTests
    {
        private static readonly TestData _testData;
        private static readonly TestFixture _fixture;
        private static readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;
        private static readonly IGameObjectFactory _gameObjectFactory;

        static HasEnchantmentsBehaviorFunctionalTests()
        {
            _testData = new TestData();
            _fixture = new TestFixture(_testData);
            _hasEnchantmentsBehaviorFactory = _fixture.LifeTimeScope.Resolve<IHasEnchantmentsBehaviorFactory>();
            _gameObjectFactory = _fixture.LifeTimeScope.Resolve<IGameObjectFactory>();
        }

        [Fact]
        private async Task AddEnchantment_ValidSingle_ContainsEnchantment()
        {
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();
            var enchantment = _gameObjectFactory.Create(new IBehavior[0]);
            await hasEnchantmentsBehavior.AddEnchantmentsAsync(new[] { enchantment });
            Assert.Contains(enchantment, hasEnchantmentsBehavior.Enchantments);
        }

        [Fact]
        private async Task AddEnchantment_ValidSingle_ExpectedEvent()
        {
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();
            var enchantment = _gameObjectFactory.Create(new IBehavior[0]);

            var enchantmentsChangedCount = 0;
            hasEnchantmentsBehavior.EnchantmentsChanged += (s, e) =>
            {
                var added = Assert.Single(e.AddedEnchantments);
                Assert.Equal(enchantment, added);
                Assert.Empty(e.RemovedEnchantments);
                enchantmentsChangedCount++;
            };

            await hasEnchantmentsBehavior.AddEnchantmentsAsync(new[] { enchantment });
            Assert.Equal(1, enchantmentsChangedCount);
        }

        [Fact]
        private async Task AddEnchantment_Empty_NoEvent()
        {
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();

            var enchantmentsChangedCount = 0;
            hasEnchantmentsBehavior.EnchantmentsChanged += (s, e) =>
            {
                enchantmentsChangedCount++;
            };

            await hasEnchantmentsBehavior.AddEnchantmentsAsync(new IGameObject[0]);
            Assert.Equal(0, enchantmentsChangedCount);
        }

        [Fact]
        private async Task RemoveEnchantment_DoesNotContain_ThrowsInvalidOperationException()
        {
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();
            var enchantment = _gameObjectFactory.Create(new IBehavior[0]);
            
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await hasEnchantmentsBehavior.RemoveEnchantmentsAsync(new[] { enchantment }));
        }

        [Fact]
        private async Task RemoveEnchantment_ContainsSingle_DoesNotContainEnchantment()
        {
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();
            var enchantment = _gameObjectFactory.Create(new IBehavior[0]);
            await hasEnchantmentsBehavior.AddEnchantmentsAsync(new[] { enchantment });
            await hasEnchantmentsBehavior.RemoveEnchantmentsAsync(new[] { enchantment });
            Assert.DoesNotContain(enchantment, hasEnchantmentsBehavior.Enchantments);
        }

        [Fact]
        private async Task RemoveEnchantment_ContainsSingle_ExpectedEvent()
        {
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();
            var enchantment = _gameObjectFactory.Create(new IBehavior[0]);
            await hasEnchantmentsBehavior.AddEnchantmentsAsync(new[] { enchantment });

            var enchantmentsChangedCount = 0;
            hasEnchantmentsBehavior.EnchantmentsChanged += (s, e) =>
            {
                var removed = Assert.Single(e.RemovedEnchantments);
                Assert.Equal(enchantment, removed);
                Assert.Empty(e.AddedEnchantments);
                enchantmentsChangedCount++;
            };

            await hasEnchantmentsBehavior.RemoveEnchantmentsAsync(new[] { enchantment });
            Assert.Equal(1, enchantmentsChangedCount);
        }

        [Fact]
        private async Task RemoveEnchantment_Empty_NoEvent()
        {
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();

            var enchantmentsChangedCount = 0;
            hasEnchantmentsBehavior.EnchantmentsChanged += (s, e) =>
            {
                enchantmentsChangedCount++;
            };

            await hasEnchantmentsBehavior.RemoveEnchantmentsAsync(new IGameObject[0]);
            Assert.Equal(0, enchantmentsChangedCount);
        }
    }
}
