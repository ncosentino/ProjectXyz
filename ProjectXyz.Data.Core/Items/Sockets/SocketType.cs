using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketType : ISocketType
    {
        #region Constructors
        private SocketType(Guid socketTypeId, Guid stringResourceId)
        {
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(stringResourceId != Guid.Empty);

            Id = socketTypeId;
            StringResourceId = stringResourceId;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get;
            private set;
        }

        public Guid StringResourceId
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public static ISocketType Create(Guid socketTypeId, Guid stringResourceId)
        {
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(stringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<ISocketType>() != null);
            return new SocketType(socketTypeId, stringResourceId);
        }
        #endregion
    }
}