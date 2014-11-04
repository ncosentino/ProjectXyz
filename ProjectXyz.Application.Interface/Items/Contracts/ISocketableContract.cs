﻿using System;
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
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.Ensures(Contract.Result<int>() >= OpenSockets);
                return default(int);
            }
        }

        public int OpenSockets
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.Ensures(Contract.Result<int>() <= TotalSockets);
                return default(int);
            }
        }

        public IReadonlyItemCollection SocketedItems
        {
            get
            {
                Contract.Ensures(Contract.Result<IReadonlyItemCollection>() != null);
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
