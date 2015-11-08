using System;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.GameObjects.Contracts
{
    [ContractClassFor(typeof(IGameObject))]
    public abstract class IGameObjectContract : IGameObject
    {
        #region Properties
        public abstract Guid Id { get; }
        #endregion

        #region Methods
        public abstract void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
