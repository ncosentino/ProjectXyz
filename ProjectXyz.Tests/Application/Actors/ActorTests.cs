using System;
using System.Linq;
using System.Collections.Generic;

using Moq;
using Xunit;

using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Tests.Application.Items.Mocks;
using ProjectXyz.Tests.Application.Enchantments.Mocks;

namespace ProjectXyz.Tests.Application.Actors
{
    public class ActorTests
    {
        [Fact(Skip="Not implemented yet")]
        public void EquipLifeEnchantedItem()
        {
            const double BASE_LIFE = 100;
            const double ADDITIONAL_LIFE = 50;

            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(ADDITIONAL_LIFE)
                .Build();

            var enchantedItem = new MockItemBuilder()
                .WithEnchantment(enchantment)
                .Build();

            var builder = new Mock<IActorBuilder>();
            var context = new Mock<IActorContext>();
            var data = new Mock<ProjectXyz.Data.Interface.Actors.IActor>();

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data.Object);

            AssertEquipItem(
                actor,
                enchantedItem,
                () =>
                {
                    Assert.Equal(BASE_LIFE, actor.CurrentLife);
                    Assert.Equal(BASE_LIFE + ADDITIONAL_LIFE, actor.MaximumLife);
                });
        }

        [Fact(Skip = "Not implemented yet")]
        public void EquipHealItem()
        {
            const double MAX_LIFE = 100;
            ////const double BASE_LIFE = 50;
            const double HEAL_AMOUNT = 1000;

            var enchantment = new MockEnchantmentBuilder()
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithStatId(ActorStats.CurrentLife)
                .WithValue(HEAL_AMOUNT)
                .Build();

            var enchantedItem = new MockItemBuilder()
                .WithEnchantment(enchantment)
                .Build();

            var builder = new Mock<IActorBuilder>();
            var context = new Mock<IActorContext>();
            var data = new Mock<ProjectXyz.Data.Interface.Actors.IActor>();

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data.Object);

            AssertEquipItem(
                actor,
                enchantedItem,
                () =>
                {
                    Assert.Equal(MAX_LIFE, actor.CurrentLife);
                    Assert.Equal(MAX_LIFE, actor.MaximumLife);
                });
        }

        public void AssertEquipItem(IActor actor, IItem item, Action validationCallback)
        {
            AssertEquipItems(
                actor,
                new IItem[] { item },
                validationCallback);
        }

        public void AssertEquipItems(IActor actor, IEnumerable<IItem> items, Action validationCallback)
        {
            Assert.NotNull(actor);
            Assert.NotNull(items);
            Assert.NotNull(validationCallback);

            foreach (var item in items)
            {
                Assert.NotNull(item);
                Assert.True(
                    actor.Equip(item),
                    "Expecting to equip " + item  + ".");
            }

            validationCallback.Invoke();
        }
    }
}
