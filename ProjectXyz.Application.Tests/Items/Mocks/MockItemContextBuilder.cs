using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Application.Tests.Items.Mocks
{
    public class MockItemContextBuilder
    {
        #region Fields
        private readonly Mock<IItemContext> _itemContext;

        private IEnchantmentCalculator _enchantmentCalculator;
        private IEnchantmentContext _enchantmentContext;
        private IStatSocketTypeRepository _statSocketTypeRepository;
        #endregion

        #region Constructors
        public MockItemContextBuilder()
        {
            _itemContext = new Mock<IItemContext>();
            _enchantmentCalculator = EnchantmentCalculator.Create();
            _enchantmentContext = new MockEnchantmentContextBuilder().Build();
            _statSocketTypeRepository = new Mock<IStatSocketTypeRepository>().Object;
        }
        #endregion

        #region Methods
        public MockItemContextBuilder WithEnchantmentCalculator(IEnchantmentCalculator enchantmentCalculator)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            _enchantmentCalculator = enchantmentCalculator;
            return this;
        }

        public MockItemContextBuilder WithEnchantmentContext(IEnchantmentContext enchantmentContext)
        {
            Contract.Requires<ArgumentNullException>(enchantmentContext != null);
            _enchantmentContext = enchantmentContext;
            return this;
        }

        public MockItemContextBuilder WithStatSocketTypeRepository(IStatSocketTypeRepository statSocketTypeRepository)
        {
            Contract.Requires<ArgumentNullException>(statSocketTypeRepository != null);
            _statSocketTypeRepository = statSocketTypeRepository;
            return this;
        }

        public IItemContext Build()
        {
            Contract.Ensures(Contract.Result<IItemContext>() != null);

            _itemContext
                .Setup(x => x.EnchantmentCalculator)
                .Returns(_enchantmentCalculator);
            _itemContext
                .Setup(x => x.EnchantmentContext)
                .Returns(_enchantmentContext);
            _itemContext
                .Setup(x => x.StatSocketTypeRepository)
                .Returns(_statSocketTypeRepository);

            return _itemContext.Object;
        }
        #endregion
    }
}
