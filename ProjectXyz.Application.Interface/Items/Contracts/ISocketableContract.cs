using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(ISocketable))]
    public abstract class ISocketableContract : ISocketable
    {
        #region Properties
        public IItemCollection SocketedItems
        {
            get
            {
                Contract.Ensures(Contract.Result<IItemCollection>() != null);
                return default(IItemCollection);
            }
        }
        #endregion

        #region Methods
        public bool Socket(IItem item)
        {
            Contract.Requires<ArgumentNullException>(item != null);
            return default(bool);
        }

        public int GetOpenSocketsForType(Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }

        public int GetTotalSocketsForType(Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }
        #endregion
    }
}
