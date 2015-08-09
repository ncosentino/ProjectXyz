using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Tests.Enchantments.Mocks
{
    public class MockEnchantmentSaverBuilder
    {
        #region Fields
        private readonly Mock<IEnchantmentSaver> _saver;
        #endregion

        #region Constructors
        public MockEnchantmentSaverBuilder()
        {
            _saver = new Mock<IEnchantmentSaver>();
        }
        #endregion

        #region Methods
        public IEnchantmentSaver Build()
        {
            Contract.Ensures(Contract.Result<IEnchantmentSaver>() != null);

            return _saver.Object;
        }
        #endregion
    }
}
