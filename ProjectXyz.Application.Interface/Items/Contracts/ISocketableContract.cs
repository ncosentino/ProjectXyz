using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(ISocketable))]
    public abstract class ISocketableContract : ISocketable
    {
        #region Properties
        public int TotalSockets
        {
            get
            {
                Contract.Requires(TotalSockets >= 0);
                Contract.Requires(OpenSockets <= TotalSockets);
                return default(int);
            }
        }

        public int OpenSockets
        {
            get
            {
                Contract.Requires(OpenSockets >= 0);
                Contract.Requires(OpenSockets <= TotalSockets);
                return default(int);
            }
        }

        public IReadonlyItemCollection SocketedItems
        {
            get
            {
                Contract.Requires<ArgumentNullException>(SocketedItems != null);
                return default(IReadonlyItemCollection);
            }
        }
        #endregion

        #region Methods
        public bool Socket(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return default(bool);
        }
        #endregion
    }
}
