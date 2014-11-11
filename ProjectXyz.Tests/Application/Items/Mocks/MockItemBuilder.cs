using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using Moq;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Tests.Application.Items.Mocks;

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
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            return WithEnchantments((IEnumerable<IEnchantment>)enchantments);
        }

        public MockItemBuilder WithEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            Contract.Requires<ArgumentNullException>(enchantments != null);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            _enchantments.AddRange(enchantments);
            return this;
        }

        public MockItemBuilder WithDurability(int maximum, int current)
        {
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

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
            Contract.Requires<ArgumentNullException>(slots != null);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            return WithEquippableSlots((IEnumerable<string>)slots);
        }

        public MockItemBuilder WithEquippableSlots(IEnumerable<string> slots)
        {
            Contract.Requires<ArgumentNullException>(slots != null);
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            _item
                .Setup(x => x.EquippableSlots)
                .Returns(slots);
            return this;
        }

        public MockItemBuilder WithRequiredSockets(int sockets)
        {
            Contract.Ensures(Contract.Result<MockItemBuilder>() != null);

            _item
                .Setup(x => x.RequiredSockets)
                .Returns(sockets);
            return this;
        }

        public IItem Build()
        {
            Contract.Ensures(Contract.Result<IItem>() != null);

            _item
                .Setup(x => x.Enchantments)
                .Returns(EnchantmentCollection.Create(_enchantments));
            _item
                .Setup(x => x.Requirements)
                .Returns(new MockRequirementsBuilder().Build());

            return _item.Object;
        }
        #endregion
    }
}
