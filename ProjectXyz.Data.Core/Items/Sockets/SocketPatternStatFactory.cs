using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketPatternStatFactory : ISocketPatternStatFactory
    {
        #region Constructors
        private SocketPatternStatFactory()
        {    
        }
        #endregion

        #region Methods
        public static ISocketPatternStatFactory Create()
        {
            var factory = new SocketPatternStatFactory();
            return factory;
        }

        public ISocketPatternStat Create(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(socketPatternDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<ISocketPatternStat>() != null);

            var itemStat = SocketPatternStat.Create(
                id,
                socketPatternDefinitionId,
                statDefinitionId,
                minimumValue,
                maximumValue);
            return itemStat;
        }
        #endregion
    }
}
