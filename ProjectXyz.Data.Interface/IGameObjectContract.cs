using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface
{
    [ContractClassFor(typeof(IGameObject))]
    public abstract class IGameObjectContract : IGameObject
    {
        #region Properties
        public abstract Guid Id { get; }
        #endregion
    }
}
