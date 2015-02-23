using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Interface.Shading
{
    public interface IObservableShade : IShade
    {
        #region Events
        event EventHandler<EventArgs> ShadeChanged;
        #endregion
    }
}
