using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketPatternEnchantment : ISocketPatternEnchantment
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _socketPatternDefinitionId;
        private readonly Guid _enchantmentDefinitionId;
        #endregion

        #region Constructors
        private SocketPatternEnchantment(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(socketPatternDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);

            _id = id;
            _socketPatternDefinitionId = socketPatternDefinitionId;
            _enchantmentDefinitionId = enchantmentDefinitionId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid SocketPatternDefinitionId { get { return _socketPatternDefinitionId; } }

        /// <inheritdoc />
        public Guid EnchantmentDefinitionId { get { return _enchantmentDefinitionId; } }
        #endregion

        #region Methods
        public static ISocketPatternEnchantment Create(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid enchantmentDefinitionId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(socketPatternDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(enchantmentDefinitionId != Guid.Empty);
            Contract.Ensures(Contract.Result<ISocketPatternEnchantment>() != null);

            var itemEnchantment = new SocketPatternEnchantment(
                id,
                socketPatternDefinitionId,
                enchantmentDefinitionId);
            return itemEnchantment;
        }
        #endregion
    }
}
