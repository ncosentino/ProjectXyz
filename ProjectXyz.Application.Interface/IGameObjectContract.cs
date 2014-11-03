using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface
{
    [ContractClassFor(typeof(IGameObject))]
    public abstract class IGameObjectContract : IGameObject
    {
        #region Properties
        public Guid Id
        {
            get
            {
                Contract.Requires<ArgumentNullException>(Id != null);
                return default(Guid);
            }
        }
        #endregion

        #region Methods
        public void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            Contract.Requires<ArgumentOutOfRangeException>(elapsedTime.TotalMilliseconds >= 0);
        }
        #endregion
    }
}
