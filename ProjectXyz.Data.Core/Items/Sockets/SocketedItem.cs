using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketedItem : ISocketedItem
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _parentItemId;
        private readonly Guid _childItemId;
        #endregion

        #region Constructors
        private SocketedItem(
            Guid id,
            Guid parentItemId,
            Guid childItemId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(parentItemId != Guid.Empty);
            Contract.Requires<ArgumentException>(childItemId != Guid.Empty);
            
            _id = id;
            _parentItemId = parentItemId;
            _childItemId = childItemId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid ParentItemId { get { return _parentItemId; } }

        /// <inheritdoc />
        public Guid ChildItemId { get { return _childItemId; } }
        #endregion

        #region Methods
        public static ISocketedItem Create(
            Guid id,
            Guid parentItemId,
            Guid childItemId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(parentItemId != Guid.Empty);
            Contract.Requires<ArgumentException>(childItemId != Guid.Empty);
            Contract.Ensures(Contract.Result<ISocketedItem>() != null);
            
            var socketedItem = new SocketedItem(
                id,
                parentItemId,
                childItemId);
            return socketedItem;
        }
        #endregion
    }
}
