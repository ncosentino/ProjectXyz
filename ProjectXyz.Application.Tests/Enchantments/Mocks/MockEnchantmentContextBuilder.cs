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
    public class MockEnchantmentContextBuilder
    {
        #region Fields
        private readonly Mock<IEnchantmentContext> _context;
        #endregion

        #region Constructors
        public MockEnchantmentContextBuilder()
        {
            _context = new Mock<IEnchantmentContext>();
        }
        #endregion

        #region Methods
        public IEnchantmentContext Build()
        {
            Contract.Ensures(Contract.Result<IEnchantmentContext>() != null);
            return _context.Object;
        }
        #endregion
    }
}
