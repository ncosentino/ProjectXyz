using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Core.GameObjects.Doors
{
    public interface IDoor
    {
        #region Properties
        bool IsOpen { get; }

        string ResourcePath { get; }
        #endregion
    }
}
