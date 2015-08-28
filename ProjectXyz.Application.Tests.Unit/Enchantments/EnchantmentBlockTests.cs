using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Enchantments
{
    [ApplicationLayer]
    [Enchantments]
    public class EnchantmentBlockTests
    {
        [Fact]
        public void Add_Single_HasItem()
        {
            // Setup
            var enchantment = new Mock<IEnchantment>();
            var enchantmentBlock = EnchantmentBlock.Create();

            // Execute
            enchantmentBlock.Add(enchantment.Object);

            // Assert
            Assert.Equal(1, enchantmentBlock.Count);
            Assert.Contains(enchantment.Object, enchantmentBlock);
        }

        [Fact]
        public void Add_Multiple_HasItems()
        {
            // Setup
            var enchantmentBlock = EnchantmentBlock.Create();

            var enchantmentsToAdd = new[]
            {
                new Mock<IEnchantment>().Object,
                new Mock<IEnchantment>().Object,
                new Mock<IEnchantment>().Object,
            };

            // Execute
            enchantmentBlock.Add(enchantmentsToAdd);

            // Assert
            Assert.Equal(enchantmentsToAdd.Length, enchantmentBlock.Count);

            for (int i = 0; i < enchantmentsToAdd.Length; ++i)
            {
                Assert.True(
                    enchantmentBlock.Contains(enchantmentsToAdd[i]),
                    "Expected enchantment " + i + " would be contained in collection");
            }
        }

        [Fact]
        public void Remove_ExistingSingle_Successful()
        {
            // Setup
            var enchantment = new Mock<IEnchantment>();
            var enchantmentBlock = EnchantmentBlock.Create();
            enchantmentBlock.Add(enchantment.Object);

            // Execute
            var result = enchantmentBlock.Remove(enchantment.Object);

            // Assert
            Assert.True(
                result,
                "Expected to remove enchantment.");

            Assert.Empty(enchantmentBlock);
        }

        [Fact]
        public void Remove_NonexistingSingle_Fails()
        {
            // Seutp
            var enchantment = new Mock<IEnchantment>();
            var enchantmentBlock = EnchantmentBlock.Create();

            // Execute
            var result = enchantmentBlock.Remove(enchantment.Object);

            // Assert
            Assert.False(
                result,
                "Expected to NOT remove enchantment.");
            Assert.Empty(enchantmentBlock);
        }

        [Fact]
        public void Remove_Multiple_Successful()
        {
            // Setup
            var enchantmentBlock = EnchantmentBlock.Create();

            var enchantmentsToRemove = new[]
            {
                new Mock<IEnchantment>().Object,
                new Mock<IEnchantment>().Object,
                new Mock<IEnchantment>().Object,
            };

            enchantmentBlock.Add(enchantmentsToRemove);

            // Execute
            var result = enchantmentBlock.Remove(enchantmentsToRemove);
            
            // Assert
            Assert.True(
                result,
                "Expected to remove enchantments.");
            Assert.Empty(enchantmentBlock);
        }

        [Fact]
        public void Add_Single_TriggersModifiedEvent()
        {
            // Setup
            var enchantment = new Mock<IEnchantment>();
            var enchantBlock = EnchantmentBlock.Create();

            bool eventTriggered = false;
            enchantBlock.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
                Assert.Equal(enchantment.Object, e.NewItems[0]);
                eventTriggered = true;
            };

            // Execute
            enchantBlock.Add(enchantment.Object);


            // Assert
            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }

        [Fact]
        public void Add_Multiple_TriggersModifiedEvent()
        {
            // Setup
            var enchantBlock = EnchantmentBlock.Create();

            var enchantmentsToAdd = new IEnchantment[]
            {
                new Mock<IEnchantment>().Object,
                new Mock<IEnchantment>().Object,
                new Mock<IEnchantment>().Object,
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

            // Execute
            enchantBlock.Add(enchantmentsToAdd);
            
            // Assert
            Assert.Equal(1, eventTriggeredCount);
        }

        [Fact]
        public void Remove_ExistingSingle_TriggersModifiedEvent()
        {
            // Setup
            var enchantment = new Mock<IEnchantment>();
            var enchantBlock = EnchantmentBlock.Create();

            enchantBlock.Add(enchantment.Object);

            bool eventTriggered = false;
            enchantBlock.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.Equal(enchantment.Object, e.OldItems[0]);
                eventTriggered = true;
            };

            // Execute
            enchantBlock.Remove(enchantment.Object);

            // Assert
            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }

        [Fact]
        public void Remove_NonexistentSingle_NoModifiedEvent()
        {
            // Setup
            var enchantment = new Mock<IEnchantment>();
            var enchantBlock = EnchantmentBlock.Create();

            bool eventTriggered = false;
            enchantBlock.CollectionChanged += (sender, e) => eventTriggered = true;
            
            // Execute
            enchantBlock.Remove(enchantment.Object);

            // Assert
            Assert.False(
                eventTriggered,
                "Not expecting the event to be triggered.");
        }

        [Fact]
        public void Remove_Multiple_TriggersModifiedEvent()
        {
            // Setup
            var enchantBlock = EnchantmentBlock.Create();

            var enchantmentsToRemove = new IEnchantment[]
            {
                new Mock<IEnchantment>().Object,
                new Mock<IEnchantment>().Object,
                new Mock<IEnchantment>().Object,
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

            // Execute
            enchantBlock.Remove(enchantmentsToRemove);
            
            // Assert
            Assert.Equal(1, eventTriggeredCount);
        }
    }
}
