using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketPatternEnchantmentFactory : ISocketPatternEnchantmentFactory
    {
        #region Constructors
        private SocketPatternEnchantmentFactory()
        {
        }
        #endregion

        #region Methods
        public static ISocketPatternEnchantmentFactory Create()
        {
            var factory = new SocketPatternEnchantmentFactory();
            return factory;
        }

        public ISocketPatternEnchantment Create(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(socketPatternDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<ISocketPatternEnchantment>() != null);

            var socketPatternEnchantment = SocketPatternEnchantment.Create(
                id,
                socketPatternDefinitionId,
                enchantmentDefinitionId);
            return socketPatternEnchantment;
        }
        #endregion
    }
}
