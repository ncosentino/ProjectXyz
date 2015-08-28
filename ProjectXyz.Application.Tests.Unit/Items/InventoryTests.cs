using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Items
{
    [ApplicationLayer]
    [Items]
    public class InventoryTests
    {
        [Fact]
        public void Add_Single_HasItem()
        {
            // Setup
            var item = new Mock<IItem>(MockBehavior.Strict);
            var inventory = Inventory.Create();

            // Execute
            inventory.Add(item.Object, 0);

            // Assert
            Assert.Equal(1, inventory.Count);
            Assert.Contains(item.Object, inventory);
        }

        [Fact]
        public void Add_Multiple_HasItems()
        {
            // Setup
            var inventory = Inventory.Create();

            var itemsToAdd = new IItem[]
            {
                new Mock<IItem>(MockBehavior.Strict).Object,
                new Mock<IItem>(MockBehavior.Strict).Object,
                new Mock<IItem>(MockBehavior.Strict).Object,
            };

            // Execute
            inventory.Add(itemsToAdd);

            // Assert
            Assert.Equal(itemsToAdd.Length, inventory.Count);

            for (int i = 0; i < itemsToAdd.Length; ++i)
            {
                Assert.True(
                    inventory.Contains(itemsToAdd[i]),
                    "Expected item " + i + " would be contained in collection");
            }
        }

        [Fact]
        public void Remove_ExistingSingle_Successful()
        {
            // Setup
            var item = new Mock<IItem>(MockBehavior.Strict);
            var inventory = Inventory.Create();
            
            inventory.Add(item.Object, 0);

            // Execute
            var result = inventory.Remove(new[] { item.Object });

            // Assert
            Assert.True(
                result,
                "Expected to remove item.");
            Assert.Empty(inventory);
        }

        [Fact]
        public void Remove_NonexistentSingle_Fails()
        {
            // Setup
            var item = new Mock<IItem>(MockBehavior.Strict);
            var inventory = Inventory.Create();

            // Execute
            var result = inventory.Remove(new[] { item.Object });

            // Assert
            Assert.False(
                result,
                "Expected to NOT remove item.");
            Assert.Empty(inventory);
        }

        [Fact]
        public void Remove_Multiple_Successful()
        {
            // Setup
            var inventory = Inventory.Create();

            var itemsToRemove = new IItem[]
            {
                new Mock<IItem>(MockBehavior.Strict).Object,
                new Mock<IItem>(MockBehavior.Strict).Object,
                new Mock<IItem>(MockBehavior.Strict).Object,
            };

            inventory.Add(itemsToRemove);

            // Execute
            var result = inventory.Remove(itemsToRemove);

            // Assert
            Assert.True(
                result,
                "Expected to remove items.");
            Assert.Empty(inventory);
        }

        [Fact]
        public void Add_Single_TriggersModifiedEvent()
        {
            // Setup
            var item = new Mock<IItem>(MockBehavior.Strict);
            var inventory = Inventory.Create();

            bool eventTriggered = false;
            inventory.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
                Assert.Equal(item.Object, e.NewItems[0]);
                eventTriggered = true;
            };

            // Execute
            inventory.Add(item.Object, 0);

            // Assert
            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }

        [Fact]
        public void Add_Multiple_TriggersModifiedEvent()
        {
            // Setup
            var inventory = Inventory.Create();

            var itemsToAdd = new IItem[]
            {
                new Mock<IItem>(MockBehavior.Strict).Object,
                new Mock<IItem>(MockBehavior.Strict).Object,
                new Mock<IItem>(MockBehavior.Strict).Object,
            };

            int eventTriggeredCount = 0;
            inventory.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
                Assert.Equal(itemsToAdd.Length, e.NewItems.Count);

                for (int i = 0; i < itemsToAdd.Length; ++i)
                {
                    Assert.True(
                        itemsToAdd[i] == e.NewItems[i],
                        "Expecting item " + i + " to be equal.");
                }

                eventTriggeredCount++;
            };

            // Execute
            inventory.Add(itemsToAdd);

            // Assert
            Assert.Equal(1, eventTriggeredCount);
        }

        [Fact]
        public void Remove_ExistingSingle_TriggersModifiedEvent()
        {
            // Setup
            var item = new Mock<IItem>(MockBehavior.Strict);
            var inventory = Inventory.Create();
            inventory.Add(item.Object, 0);

            bool eventTriggered = false;
            inventory.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.Equal(item.Object, e.OldItems[0]);
                eventTriggered = true;
            };

            // Execute
            inventory.Remove(new[] { item.Object });

            // Assert
            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }

        [Fact]
        public void Remove_NonexistentSingle_NoModifiedEvent()
        {
            // Setup
            var item = new Mock<IItem>(MockBehavior.Strict);
            var inventory = Inventory.Create();

            bool eventTriggered = false;
            inventory.CollectionChanged += (sender, e) => eventTriggered = true;
            
            // Execute
            inventory.Remove(new[] { item.Object });

            // Assert
            Assert.False(
                eventTriggered,
                "Not expecting the event to be triggered.");
        }

        [Fact]
        public void Remove_Multiple_TriggersModifiedEvent()
        {
            // Setup
            var inventory = Inventory.Create();

            var itemsToRemove = new IItem[]
            {
                new Mock<IItem>(MockBehavior.Strict).Object,
                new Mock<IItem>(MockBehavior.Strict).Object,
                new Mock<IItem>(MockBehavior.Strict).Object,
            };
            inventory.Add(itemsToRemove);

            int eventTriggeredCount = 0;
            inventory.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.Equal(itemsToRemove.Length, e.OldItems.Count);

                for (int i = 0; i < itemsToRemove.Length; ++i)
                {
                    Assert.True(
                        itemsToRemove[i] == e.OldItems[i],
                        "Expecting item " + i + " to be equal.");
                }

                eventTriggeredCount++;
            };

            // Execute
            inventory.Remove(itemsToRemove);
            
            // Assert
            Assert.Equal(1, eventTriggeredCount);
        }
    }
}
