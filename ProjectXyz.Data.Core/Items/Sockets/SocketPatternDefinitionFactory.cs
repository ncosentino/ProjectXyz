using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketPatternDefinitionFactory : ISocketPatternDefinitionFactory
    {
        #region Constructors
        private SocketPatternDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static ISocketPatternDefinitionFactory Create()
        {
            var factory = new SocketPatternDefinitionFactory();
            return factory;
        }

        public ISocketPatternDefinition Create(
            Guid id,
            Guid nameStringResourceId,
            Guid? inventoryGraphicResourceId,
            Guid? magicTypeId,
            float chance)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(inventoryGraphicResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(magicTypeId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(chance >= 0 && chance < 1f);
            Contract.Ensures(Contract.Result<ISocketPatternDefinition>() != null);

            var socketPatternDefinition = SocketPatternDefinition.Create(
                id,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                chance);
            return socketPatternDefinition;
        }
        #endregion
    }
}