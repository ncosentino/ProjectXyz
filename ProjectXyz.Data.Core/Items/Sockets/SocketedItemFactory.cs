using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketedItemFactory : ISocketedItemFactory
    {
        #region Constructors
        private SocketedItemFactory()
        {
        }
        #endregion

        #region Methods
        public static ISocketedItemFactory Create()
        {
            var factory = new SocketedItemFactory();
            return factory;
        }

        /// <inheritdoc />
        public ISocketedItem Create(
            Guid id,
            Guid parentItemId,
            Guid childItemId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(parentItemId != Guid.Empty);
            Contract.Requires<ArgumentException>(childItemId != Guid.Empty);
            Contract.Ensures(Contract.Result<ISocketedItem>() != null);
            
            var socketedItem = SocketedItem.Create(
                id,
                parentItemId,
                childItemId);
            return socketedItem;
        }
        #endregion
    }
}
