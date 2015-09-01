using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items.Affixes
{
    public interface IItemAffixGenerator
    {
        #region Methods
        IEnumerable<IItemAffix> GenerateRandom(IRandom randomizer, int level, Guid magicTypeId);

        IItemAffix GeneratePrefix(IRandom randomizer, int level, Guid magicTypeId);

        IItemAffix GenerateSuffix(IRandom randomizer, int level, Guid magicTypeId);
        #endregion
    }
}
