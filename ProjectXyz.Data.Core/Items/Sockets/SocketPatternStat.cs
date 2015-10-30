using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Core.Items.Sockets
{
    public sealed class SocketPatternStat : ISocketPatternStat
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _socketPatternDefinitionId;
        private readonly Guid _statDefinitionId;
        private readonly double _minimumValue;
        private readonly double _maximumValue;
        #endregion

        #region Constructors
        private SocketPatternStat(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(socketPatternDefinitionId != Guid.Empty);
            Contract.Requires<ArgumentException>(statDefinitionId != Guid.Empty);

            _id = id;
            _socketPatternDefinitionId = socketPatternDefinitionId;
            _statDefinitionId = statDefinitionId;
            _minimumValue = minimumValue;
            _maximumValue = maximumValue;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid SocketPatternDefinitionId { get { return _socketPatternDefinitionId; } }

        /// <inheritdoc />
        public Guid StatDefinitionId { get { return _statDefinitionId; } }

        /// <inheritdoc />
        public double MinimumValue { get { return _minimumValue; } }

        /// <inheritdoc />
        public double MaximumValue { get { return _maximumValue; } }
        #endregion

        #region Methods
        public static ISocketPatternStat Create(
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

            var itemStat = new SocketPatternStat(
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
