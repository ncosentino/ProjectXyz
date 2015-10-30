using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketPatternDefinition : ISocketPatternDefinition
    {
        #region Fields
        private readonly Guid _id;
        private readonly float _chance;
        private readonly Guid? _inventoryGraphicResourceId;
        private readonly Guid? _magicTypeId;
        private readonly Guid _nameStringResourceId;
        #endregion

        #region Constructors
        private SocketPatternDefinition(
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

            _id = id;
            _nameStringResourceId = nameStringResourceId;
            _inventoryGraphicResourceId = inventoryGraphicResourceId;
            _magicTypeId = magicTypeId;
            _chance = chance;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public float Chance { get { return _chance; } }

        /// <inheritdoc />
        public Guid? InventoryGraphicResourceId { get { return _inventoryGraphicResourceId; } }

        /// <inheritdoc />
        public Guid? MagicTypeId { get { return _magicTypeId; } }

        /// <inheritdoc />
        public Guid NameStringResourceId { get { return _nameStringResourceId; } }
        #endregion

        #region Methods
        public static ISocketPatternDefinition Create(
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

            var socketPatternDefinition = new SocketPatternDefinition(
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