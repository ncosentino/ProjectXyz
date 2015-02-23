using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Shading
{
    public interface IMutableShade : IObservableShade
    {
        #region Properties
        new float Red { get; set; }

        new float Blue { get; set; }

        new float Green { get; set; }

        new float Alpha { get; set; }
        #endregion
    }
}
