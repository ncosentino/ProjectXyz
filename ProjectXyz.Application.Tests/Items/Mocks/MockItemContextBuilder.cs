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
using ProjectXyz.Application.Tests.Enchantments.Mocks;

namespace ProjectXyz.Application.Tests.Items.Mocks
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
            Contract.Ensures(Contract.Result<IItemContext>() != null);

            _itemContext
                .Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());
            _itemContext
                .Setup(x => x.EnchantmentContext)
                .Returns(new MockEnchantmentContextBuilder().Build());

            return _itemContext.Object;
        }
        #endregion
    }
}
