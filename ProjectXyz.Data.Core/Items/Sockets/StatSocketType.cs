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
        private readonly Guid _statId;
        private readonly Guid _socketTypeId;
        #endregion

        #region Constructors
        private StatSocketType(Guid statId, Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);

            _statId = statId;
            _socketTypeId = socketTypeId;
        }
        #endregion

        #region Properties
        public Guid StatId
        {
            get { return _statId; }
        }

        public Guid SocketTypeId
        {
            get { return _socketTypeId; }
        }
        #endregion

        #region Methods
        public static IStatSocketType Create(Guid statId, Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IStatSocketType>() != null);

            return new StatSocketType(statId, socketTypeId);
        }
        #endregion
    }
}
