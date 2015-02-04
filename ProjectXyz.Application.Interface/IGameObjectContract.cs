using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface
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
