using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface
{
    [ContractClass(typeof(IGameObjectContract))]
    public interface IGameObject : IUpdateElapsedTime
    {
        #region Properties
        Guid Id { get; }
        #endregion
    }
}
