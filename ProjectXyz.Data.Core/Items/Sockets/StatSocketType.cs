using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class StatSocketType : IStatSocketType
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _statDefinitionId;
        private readonly Guid _socketTypeId;
        #endregion

        #region Constructors
        private StatSocketType(
            Guid id,
            Guid statDefinitionId, 
            Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);

            _id = id;
            _statDefinitionId = statDefinitionId;
            _socketTypeId = socketTypeId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id
        {
            get { return _id; }
        }

        /// <inheritdoc />
        public Guid StatDefinitionId
        {
            get { return _statDefinitionId; }
        }

        /// <inheritdoc />
        public Guid SocketTypeId
        {
            get { return _socketTypeId; }
        }
        #endregion

        #region Methods
        public static IStatSocketType Create(
            Guid id,
            Guid statDefinitionId,
            Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IStatSocketType>() != null);

            var statSocketType = new StatSocketType(
                id, 
                statDefinitionId, 
                socketTypeId);
            return statSocketType;
        }
        #endregion
    }
}
