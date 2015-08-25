using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Plugins.Enchantments.OneShotNegate;

namespace ProjectXyz.Application.Tests.Items.Mocks
{
    public class MockItemContextBuilder
    {
        #region Fields
        private readonly Mock<IItemContext> _itemContext;

        private IEnchantmentCalculator _enchantmentCalculator;
        private IStatSocketTypeRepository _statSocketTypeRepository;
        private IEnchantmentFactory _enchantmentFactory;
        #endregion

        #region Constructors
        public MockItemContextBuilder()
        {
            _itemContext = new Mock<IItemContext>();
            _enchantmentCalculator = CreateEnchantmentCalculator();
            _statSocketTypeRepository = new Mock<IStatSocketTypeRepository>().Object;

            _enchantmentFactory = EnchantmentFactory.Create();
        }
        #endregion

        #region Methods
        public MockItemContextBuilder WithEnchantmentCalculator(IEnchantmentCalculator enchantmentCalculator)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            _enchantmentCalculator = enchantmentCalculator;
            return this;
        }
        
        public MockItemContextBuilder WithStatSocketTypeRepository(IStatSocketTypeRepository statSocketTypeRepository)
        {
            Contract.Requires<ArgumentNullException>(statSocketTypeRepository != null);
            _statSocketTypeRepository = statSocketTypeRepository;
            return this;
        }

        public MockItemContextBuilder WithEnchantmentFactory(IEnchantmentFactory enchantmentFactory)
        {
            Contract.Requires<ArgumentNullException>(enchantmentFactory != null);
            _enchantmentFactory = enchantmentFactory;
            return this;
        }

        public IItemContext Build()
        {
            Contract.Ensures(Contract.Result<IItemContext>() != null);

            _itemContext
                .Setup(x => x.EnchantmentCalculator)
                .Returns(_enchantmentCalculator);
            _itemContext
                .Setup(x => x.EnchantmentFactory)
                .Returns(_enchantmentFactory);
            _itemContext
                .Setup(x => x.StatSocketTypeRepository)
                .Returns(_statSocketTypeRepository);

            return _itemContext.Object;
        }

        private IEnchantmentCalculator CreateEnchantmentCalculator()
        {
            var statusNegationRepository = new Mock<IStatusNegationRepository>();
            statusNegationRepository
                .Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<IStatusNegation>());

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var stringExpressionEvaluator = DataTableExpressionEvaluator.Create();
            var expressionEvaluator = ExpressionEvaluator.Create(stringExpressionEvaluator.Evaluate);

            return EnchantmentCalculator.Create(
                enchantmentContext.Object,
                EnchantmentCalculatorResultFactory.Create(),
                new[]
                {
                    OneShotNegateEnchantmentTypeCalculator.Create(statusNegationRepository.Object),
                    ExpressionEnchantmentTypeCalculator.Create(
                        StatFactory.Create(),
                        expressionEvaluator),
                });
        }
        #endregion
    }
}
