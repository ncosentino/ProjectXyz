﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemDefinitionStat : IStatRange
    {
        #region Properties
        Guid Id { get; }

        Guid ItemDefinitionId { get; }
        #endregion
    }
}
