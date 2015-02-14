using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Xunit;
using Moq;

using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Application.Tests.Items.Mocks;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Tests.Items
{
    [ApplicationLayer]
    [Items]
    public class InventoryTests
    {
        [Fact]
        public void Inventory_AddSingle_HasItem()
        {
            var item = new MockItemBuilder().Build();
            var inventory = Inventory.Create();

            inventory.Add(item);

            Assert.Equal(1, inventory.Count);
            Assert.Contains(item, inventory);
        }

        [Fact]
        public void Inventory_AddMultiple_HasItems()
        {
            var inventory = Inventory.Create();

            var itemsToAdd = new IItem[]
            {
                new MockItemBuilder().Build(),
                new MockItemBuilder().Build(),
                new MockItemBuilder().Build(),
            };

            inventory.Add(itemsToAdd);

            Assert.Equal(itemsToAdd.Length, inventory.Count);

            for (int i = 0; i < itemsToAdd.Length; ++i)
            {
                Assert.True(
                    inventory.Contains(itemsToAdd[i]),
                    "Expected item " + i + " would be contained in collection");
            }
        }

        [Fact]
        public void Inventory_RemoveExistingSingle_Successful()
        {
            var item = new MockItemBuilder().Build();
            var inventory = Inventory.Create();
            inventory.Add(item);

            Assert.True(
                inventory.Remove(item),
                "Expected to remove item.");
            Assert.Empty(inventory);
        }

        [Fact]
        public void Inventory_RemoveNonexistentSingle_Fails()
        {
            var item = new MockItemBuilder().Build();
            var inventory = Inventory.Create();

            Assert.False(
                inventory.Remove(item),
                "Expected to NOT remove item.");
            Assert.Empty(inventory);
        }

        [Fact]
        public void Inventory_RemoveMultiple_Successful()
        {
            var inventory = Inventory.Create();

            var itemsToRemove = new IItem[]
            {
                new MockItemBuilder().Build(),
                new MockItemBuilder().Build(),
                new MockItemBuilder().Build(),
            };

            inventory.Add(itemsToRemove);

            Assert.True(
                inventory.Remove(itemsToRemove),
                "Expected to remove items.");
            Assert.Empty(inventory);
        }

        [Fact]
        public void Inventory_AddSingle_TriggersModifiedEvent()
        {
            var item = new MockItemBuilder().Build();
            var inventory = Inventory.Create();

            bool eventTriggered = false;
            inventory.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
                Assert.Equal(item, e.NewItems[0]);
                eventTriggered = true;
            };
            
            inventory.Add(item);

            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }

        [Fact]
        public void Inventory_AddMultiple_TriggersModifiedEvent()
        {
            var inventory = Inventory.Create();

            var itemsToAdd = new IItem[]
            {
                new MockItemBuilder().Build(),
                new MockItemBuilder().Build(),
                new MockItemBuilder().Build(),
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

            inventory.Add(itemsToAdd);
            Assert.Equal(1, eventTriggeredCount);
        }

        [Fact]
        public void Inventory_RemoveExistingSingle_TriggersModifiedEvent()
        {
            var item = new MockItemBuilder().Build();
            var inventory = Inventory.Create();
            inventory.Add(item);

            bool eventTriggered = false;
            inventory.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.Equal(item, e.OldItems[0]);
                eventTriggered = true;
            };

            inventory.Remove(item);

            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }

        [Fact]
        public void Inventory_RemoveNonexistentSingle_NoModifiedEvent()
        {
            var item = new MockItemBuilder().Build();
            var inventory = Inventory.Create();

            bool eventTriggered = false;
            inventory.CollectionChanged += (sender, e) => eventTriggered = true;
            inventory.Remove(item);

            Assert.False(
                eventTriggered,
                "Not expecting the event to be triggered.");
        }

        [Fact]
        public void Inventory_RemoveMultiple_TriggersModifiedEvent()
        {
            var inventory = Inventory.Create();

            var itemsToRemove = new IItem[]
            {
                new MockItemBuilder().Build(),
                new MockItemBuilder().Build(),
                new MockItemBuilder().Build(),
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

            inventory.Remove(itemsToRemove);
            Assert.Equal(1, eventTriggeredCount);
        }
    }
}
