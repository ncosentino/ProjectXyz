using System;
using System.Collections.Generic;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items.Affixes;

namespace ProjectXyz.Plugins.Items.Magic
{
    public interface IMagicAffixGenerator
    {
        IEnumerable<IItemAffix> GenerateAffixes(
            IRandom randomizer,
            int level,
            Guid magicTypeId);
    }
}