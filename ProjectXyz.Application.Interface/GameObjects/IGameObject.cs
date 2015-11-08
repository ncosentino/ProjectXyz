using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.GameObjects.Contracts;

namespace ProjectXyz.Application.Interface.GameObjects
{
    [ContractClass(typeof(IGameObjectContract))]
    public interface IGameObject : IUpdateElapsedTime
    {
        #region Properties
        Guid Id { get; }
        #endregion
    }
}
