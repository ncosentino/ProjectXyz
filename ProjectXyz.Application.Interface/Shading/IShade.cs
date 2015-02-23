using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Shading
{
    public interface IShade
    {
        #region Properties
        float Red { get; }

        float Blue { get; }

        float Green { get; }

        float Alpha { get; }
        #endregion
    }
}
