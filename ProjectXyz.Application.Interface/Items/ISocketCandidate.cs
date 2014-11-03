using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(ISocketCandidateContract))]
    public interface ISocketCandidate
    {
        #region Properties
        int RequiredSockets { get; }
        #endregion
    }
}
