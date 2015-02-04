using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Xunit;
using Moq;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Application.Tests.Enchantments.Mocks;

namespace ProjectXyz.Application.Tests.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentBlockTests
    {
        [Fact]
        public void EnchantmentBlock_AddSingle_HasItem()
        {
            var enchantment = new MockEnchantmentBuilder().Build();
            var enchantmentBlock = EnchantmentBlock.Create();

            enchantmentBlock.Add(enchantment);

            Assert.Equal(1, enchantmentBlock.Count);
            Assert.Contains(enchantment, enchantmentBlock);
        }

        [Fact]
        public void EnchantmentBlock_AddMultiple_HasItems()
        {
            var enchantmentBlock = EnchantmentBlock.Create();

            var enchantmentsToAdd = new IEnchantment[]
            {
                new MockEnchantmentBuilder().Build(),
                new MockEnchantmentBuilder().Build(),
                new MockEnchantmentBuilder().Build(),
            };

            enchantmentBlock.Add(enchantmentsToAdd);

            Assert.Equal(enchantmentsToAdd.Length, enchantmentBlock.Count);

            for (int i = 0; i < enchantmentsToAdd.Length; ++i)
            {
                Assert.True(
                    enchantmentBlock.Contains(enchantmentsToAdd[i]),
                    "Expected enchantment " + i + " would be contained in collection");
            }
        }

        [Fact]
        public void EnchantmentBlock_RemoveExistingSingle_Successful()
        {
            var enchantment = new MockEnchantmentBuilder().Build();
            var enchantmentBlock = EnchantmentBlock.Create();
            enchantmentBlock.Add(enchantment);

            Assert.True(
                enchantmentBlock.Remove(enchantment),
                "Expected to remove enchantment.");
            Assert.Empty(enchantmentBlock);
        }

        [Fact]
        public void EnchantmentBlock_RemoveNonexistingSingle_Fails()
        {
            var enchantment = new MockEnchantmentBuilder().Build();
            var enchantmentBlock = EnchantmentBlock.Create();

            Assert.False(
                enchantmentBlock.Remove(enchantment),
                "Expected to NOT remove enchantment.");
            Assert.Empty(enchantmentBlock);
        }

        [Fact]
        public void EnchantmentBlock_RemoveMultiple_Successful()
        {
            var enchantmentBlock = EnchantmentBlock.Create();

            var enchantmentsToRemove = new IEnchantment[]
            {
                new MockEnchantmentBuilder().Build(),
                new MockEnchantmentBuilder().Build(),
                new MockEnchantmentBuilder().Build(),
            };

            enchantmentBlock.Add(enchantmentsToRemove);

            Assert.True(
                enchantmentBlock.Remove(enchantmentsToRemove),
                "Expected to remove enchantments.");
            Assert.Empty(enchantmentBlock);
        }

        [Fact]
        public void EnchantmentBlock_AddSingle_TriggersModifiedEvent()
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
        public void EnchantmentBlock_AddMultiple_TriggersModifiedEvent()
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
        public void EnchantmentBlock_RemoveExistingSingle_TriggersModifiedEvent()
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
        public void EnchantmentBlock_RemoveNonexistentSingle_NoModifiedEvent()
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
        public void EnchantmentBlock_RemoveMultiple_TriggersModifiedEvent()
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
