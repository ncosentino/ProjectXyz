using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketType : ISocketType
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _nameStringResourceId;
        #endregion

        #region Constructors
        private SocketType(
            Guid id, 
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);

            _id = id;
            _nameStringResourceId = nameStringResourceId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid NameStringResourceId { get { return _nameStringResourceId; } }
        #endregion

        #region Methods
        public static ISocketType Create(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<ISocketType>() != null);

            var socketType = new SocketType(id, nameStringResourceId);
            return socketType;
        }
        #endregion
    }
}