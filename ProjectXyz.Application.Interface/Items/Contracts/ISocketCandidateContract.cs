using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(ISocketCandidate))]
    public abstract class ISocketCandidateContract : ISocketCandidate
    {
        #region Properties
        public int RequiredSockets
        {
            get
            {
                Contract.Requires(RequiredSockets >= 0);
                return default(int);
            }
        }
        #endregion
    }
}
