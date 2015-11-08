using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Application.Interface.GameObjects;
using ProjectXyz.Application.Interface.Shading;

namespace ProjectXyz.Application.Interface.Maps
{
    public interface IMap : IGameObject
    {
        #region Properties
        // FIXME: this *IS* the resource
        string ResourceName { get; }

        bool IsInterior { get; }

        IObservableShade AmbientLight { get; }
        #endregion

        #region Methods
        TGameObject FindGameObject<TGameObject>(Guid id)
            where TGameObject : IGameObject;
        #endregion
    }
}
