using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments.Contracts
{
    [ContractClassFor(typeof(IEnchantmentRepository))]
    public abstract class IEnchantmentRepositoryContract : IEnchantmentRepository
    {
        #region Methods
        public IEnumerable<IEnchantment> GenerateRandom(int minimum, int maximum, int level, Random randomizer)
        {
            Contract.Requires<ArgumentOutOfRangeException>(minimum >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(minimum <= maximum);
            Contract.Requires<ArgumentOutOfRangeException>(level >= 0);
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            return default(IEnumerable<IEnchantment>);
        }

        public IEnchantment Generate(Guid id, Random randomizer)
        {
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Ensures(Contract.Result<IEnchantment>() != null);

            return default(IEnchantment);
        }
        #endregion
    }
}
