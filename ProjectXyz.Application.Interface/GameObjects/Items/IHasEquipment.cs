using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Interface.GameObjects.Items
{
    public interface IHasEquipment : IGameObject
    {
        #region Methods
        IMutableEquipment Equipment { get; }
        #endregion
    }
}
