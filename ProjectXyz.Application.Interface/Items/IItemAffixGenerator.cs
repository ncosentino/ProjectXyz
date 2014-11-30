using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemAffixGenerator
    {
        #region Methods
        IEnumerable<IItemAffix> GenerateRandom(IRandom randomizer, int level, Guid magicTypeId);

        INamedItemAffix GeneratePrefix(IRandom randomizer, int level, Guid magicTypeId);

        INamedItemAffix GenerateSuffix(IRandom randomizer, int level, Guid magicTypeId);
        #endregion
    }
}
