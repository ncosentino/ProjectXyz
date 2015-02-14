﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface
{
    [ContractClass(typeof(IGameObjectContract))]
    public interface IGameObject
    {
        #region Properties
        Guid Id { get; }
        #endregion
    }
}
