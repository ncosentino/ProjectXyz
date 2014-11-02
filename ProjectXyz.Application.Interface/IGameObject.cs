using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface
{
    [ContractClass(typeof(IGameObjectContract))]
    public interface IGameObject : ProjectXyz.Interface.IGameObject, IUpdateElapsedTime
    {
    }
}
