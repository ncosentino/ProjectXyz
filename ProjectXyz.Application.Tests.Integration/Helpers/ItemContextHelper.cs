using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Application.Tests.Integration.Helpers
{
    public static class ItemContextHelper
    {
        #region Methods
        public static IItemContext CreateItemContext(
            IStatusNegationRepository statusNegationRepository = null,
            IStatSocketTypeRepository statSocketTypeRepository = null)
        {
            var enchantmentCalculator = EnchantmentCalculatorHelper.CreateEnchantmentCalculator(statusNegationRepository);

            var enchantmentFactory = EnchantmentFactory.Create();

            if (statSocketTypeRepository == null)
            {
                var mockstatSocketTypeRepository = new Mock<IStatSocketTypeRepository>(MockBehavior.Strict);
                mockstatSocketTypeRepository
                    .Setup(x => x.GetAll())
                    .Returns(Enumerable.Empty<IStatSocketType>());
                statSocketTypeRepository = mockstatSocketTypeRepository.Object;
            }

            var itemContext = ItemContext.Create(
                enchantmentCalculator,
                enchantmentFactory,
                statSocketTypeRepository);
            return itemContext;
        }
        #endregion
    }
}
