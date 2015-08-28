using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class StatSocketTypeFactory : IStatSocketTypeFactory
    {
        #region Constructors
        private StatSocketTypeFactory()
        {
        }
        #endregion

        #region Methods
        public static IStatSocketTypeFactory Create()
        {
            var factory = new StatSocketTypeFactory();
            return factory;
        }

        public IStatSocketType Create(
            Guid id,
            Guid statDefinitionId,
            Guid socketTypeId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(socketTypeId != Guid.Empty);
            Contract.Ensures(Contract.Result<IStatSocketType>() != null);

            var statSocketType = StatSocketType.Create(
                id, 
                statDefinitionId, 
                socketTypeId);
            return statSocketType;
        }
        #endregion
    }
}
