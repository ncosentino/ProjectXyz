using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Application.Core.GameObjects.Doors
{
    public interface IObservableDoor : IDoor
    {
        #region Events
        event EventHandler<EventArgs> OpenChanged;

        event EventHandler<EventArgs> ResourcePathChanged;
        #endregion
    }
}
