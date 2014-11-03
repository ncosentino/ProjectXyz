﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(ISocketableContract))]
    public interface ISocketable
    {
        #region Properties
        int TotalSockets { get; }

        int OpenSockets { get; }

        IReadonlyItemCollection SocketedItems { get; }
        #endregion

        #region Methods
        bool Socket(IItem item);
        #endregion
    }
}