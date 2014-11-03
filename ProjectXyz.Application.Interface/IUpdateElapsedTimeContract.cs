using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface
{
    [ContractClassFor(typeof(IUpdateElapsedTime))]
    public abstract class IUpdateElapsedTimeContract : IUpdateElapsedTime
    {
        #region Methods
        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            Contract.Requires<ArgumentOutOfRangeException>(elapsedTime.TotalMilliseconds >= 0);
        }
        #endregion
    }
}
