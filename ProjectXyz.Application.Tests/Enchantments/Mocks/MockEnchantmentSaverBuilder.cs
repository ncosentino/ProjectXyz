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
