using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Interactions;

namespace ProjectXyz.Application.Core.GameObjects.Doors
{
    public interface IMutableDoor : IObservableDoor, IInteractable, IOpenable, ICloseable
    {
        #region Properties
        new string ResourcePath { get; set; }
        #endregion
    }
}
