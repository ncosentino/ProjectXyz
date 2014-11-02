using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface
{
    public interface IUpdateElapsedTime
    {
        #region Methods
        void UpdateElapsedTime(TimeSpan elapsedTime);
        #endregion
    }
}
