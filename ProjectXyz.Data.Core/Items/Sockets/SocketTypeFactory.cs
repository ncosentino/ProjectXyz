using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketTypeFactory : ISocketTypeFactory
    {
        #region Constructors
        private SocketTypeFactory()
        {
        }
        #endregion

        #region Methods
        public static ISocketTypeFactory Create()
        {
            var factory = new SocketTypeFactory();
            return factory;
        }

        /// <inheritdoc />
        public ISocketType Create(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<ISocketType>() != null);

            var socketType = SocketType.Create(id, nameStringResourceId);
            return socketType;
        }
        #endregion
    }
}