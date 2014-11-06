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
        public MockItemBuilder WithEnchantment(IEnchantment enchantment)
        {
            _enchantments.Add(enchantment);
            return this;
        }

        public MockItemBuilder WithEnchantments(IEnumerable<IEnchantment> enchantments)
        {
            _enchantments.AddRange(enchantments);
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
