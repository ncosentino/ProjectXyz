using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Moq;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Tests.Application.Enchantments.Mocks;

namespace ProjectXyz.Tests.Application.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentBlockTests
    {
        [Fact]
        public void AddTriggersModified()
        {
            var enchantment = new MockEnchantmentBuilder().Build();
            var enchantBlock = EnchantmentBlock.Create();

            bool eventTriggered = false;
            enchantBlock.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
                Assert.Equal(enchantment, e.NewItems[0]);
                eventTriggered = true;
            };
            
            enchantBlock.Add(enchantment);

            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }

        [Fact]
        public void AddMultipleTriggersModified()
        {
            var enchantBlock = EnchantmentBlock.Create();

            var enchantmentsToAdd = new IEnchantment[]
            {
                new MockEnchantmentBuilder().Build(),
                new MockEnchantmentBuilder().Build(),
                new MockEnchantmentBuilder().Build(),
            };

            int eventTriggeredCount = 0;
            enchantBlock.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
                Assert.Equal(enchantmentsToAdd.Length, e.NewItems.Count);

                for (int i = 0; i < enchantmentsToAdd.Length; ++i)
                {
                    Assert.True(
                        enchantmentsToAdd[i] == e.NewItems[i],
                        "Expecting item " + i + " to be equal.");
                }

                eventTriggeredCount++;
            };

            enchantBlock.Add(enchantmentsToAdd);
            Assert.Equal(1, eventTriggeredCount);
        }

        [Fact]
        public void RemoveTriggersModified()
        {
            var enchantment = new MockEnchantmentBuilder().Build();
            var enchantBlock = EnchantmentBlock.Create();

            enchantBlock.Add(enchantment);

            bool eventTriggered = false;
            enchantBlock.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.Equal(enchantment, e.OldItems[0]);
                eventTriggered = true;
            };

            enchantBlock.Remove(enchantment);

            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }

        [Fact]
        public void RemoveNonexistentNotModified()
        {
            var enchantment = new MockEnchantmentBuilder().Build();
            var enchantBlock = EnchantmentBlock.Create();

            bool eventTriggered = false;
            enchantBlock.CollectionChanged += (sender, e) => eventTriggered = true;
            enchantBlock.Remove(enchantment);

            Assert.False(
                eventTriggered,
                "Not expecting the event to be triggered.");
        }

        [Fact]
        public void RemoveMultipleTriggersModified()
        {
            var enchantBlock = EnchantmentBlock.Create();

            var enchantmentsToRemove = new IEnchantment[]
            {
                new MockEnchantmentBuilder().Build(),
                new MockEnchantmentBuilder().Build(),
                new MockEnchantmentBuilder().Build(),
            };
            enchantBlock.Add(enchantmentsToRemove);

            int eventTriggeredCount = 0;
            enchantBlock.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.Equal(enchantmentsToRemove.Length, e.OldItems.Count);

                for (int i = 0; i < enchantmentsToRemove.Length; ++i)
                {
                    Assert.True(
                        enchantmentsToRemove[i] == e.OldItems[i],
                        "Expecting item " + i + " to be equal.");
                }

                eventTriggeredCount++;
            };

            enchantBlock.Remove(enchantmentsToRemove);
            Assert.Equal(1, eventTriggeredCount);
        }
    }
}
