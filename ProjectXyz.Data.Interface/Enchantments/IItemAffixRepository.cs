﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IItemAffixRepository
    {
        #region Methods
        IEnumerable<IItemAffix> GenerateRandom(int minimum, int maximum, Guid magicTypeId, int level, Random randomizer);

        IItemAffix GenerateRandom(bool prefix, Guid magicTypeId, int level, Random randomizer);

        IItemAffix Generate(Guid id, Random randomizer);
        #endregion
    }
}
