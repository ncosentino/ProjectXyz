using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Moq;

using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Tests.Application.Items.Mocks;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Tests.Application.Items
{
    [ApplicationLayer]
    [Items]
    public class InventoryTests
    {
        [Fact]
        public void AddTriggersModified()
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
        public void AddMultipleTriggersModified()
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
        public void RemoveTriggersModified()
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
        public void RemoveNonexistentNotModified()
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
        public void RemovedMultipleTriggersModified()
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
