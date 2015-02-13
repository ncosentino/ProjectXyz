﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Items
{
    public interface IItemCollection : IEnumerable<IItem>
    {
        #region Properties
        int Count { get; }
        #endregion
    }
}
