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

namespace ProjectXyz.Tests.Application.Items
{
    public class EnchantmentBlockTests
    {
        [Fact]
        public void AddTriggersModified()
        {
            var enchantment = new Mock<IEnchantment>();
            var enchantBlock = EnchantmentBlock.Create();

            bool eventTriggered = false;
            enchantBlock.CollectionChanged += (sender, e) =>
            {
                Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
                Assert.Equal(enchantment.Object, e.NewItems[0]);
                eventTriggered = true;
            };
            
            enchantBlock.Add(enchantment.Object);

            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }

        [Fact]
        public void RemoveTriggersModified()
        {
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

            enchantBlock.Remove(enchantment.Object);

            Assert.True(
                eventTriggered,
                "Expecting the event to be triggered.");
        }
    }
}
