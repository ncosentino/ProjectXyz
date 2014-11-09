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
    public class MockItemContextBuilder
    {
        #region Fields
        private readonly Mock<IItemContext> _itemContext;
        #endregion

        #region Constructors
        public MockItemContextBuilder()
        {
            _itemContext = new Mock<IItemContext>();
        }
        #endregion

        #region Methods
        public IItemContext Build()
        {
            _itemContext
                .Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            return _itemContext.Object;
        }
        #endregion
    }
}
