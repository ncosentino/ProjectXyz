using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Tests.Application.Items.Mocks
{
    public class MockItemBuilder
    {
        #region Fields
        private readonly Mock<IItem> _item;
        private readonly List<IEnchantment> _enchantments;
        #endregion

        #region Constructors
        public MockItemBuilder()
        {
            _item = new Mock<IItem>();
            _enchantments = new List<IEnchantment>();
        }
        #endregion

        #region Methods
        public MockItemBuilder WithEnchantments(params IEnchantment[] enchantments)
        {
            return WithEnchantments((IEnumerable<IEnchantment>)enchantments);
        }

        public MockItemBuilder WithEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            _enchantments.AddRange(enchantments);
            return this;
        }

        public MockItemBuilder WithDurability(int maximum, int current)
        {
            var durability = new Mock<IDurability>();
            durability
                .Setup(x => x.Maximum)
                .Returns(maximum);
            durability
                .Setup(x => x.Current)
                .Returns(current);            

            _item
                .Setup(x => x.Durability)
                .Returns(durability.Object);
            return this;
        }

        public MockItemBuilder WithEquippableSlots(params string[] slots)
        {
            return WithEquippableSlots((IEnumerable<string>)slots);
        }

        public MockItemBuilder WithEquippableSlots(IEnumerable<string> slots)
        {
            _item
                .Setup(x => x.EquippableSlots)
                .Returns(slots);
            return this;
        }

        public IItem Build()
        {
            _item
                .Setup(x => x.Enchantments)
                .Returns(EnchantmentCollection.Create(_enchantments));

            return _item.Object;
        }
        #endregion
    }
}
