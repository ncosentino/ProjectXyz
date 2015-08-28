using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Core.Items.Affixes
{
    public sealed class ItemAffixDefinition : IItemAffixDefinition
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _nameStringResourceId;
        private readonly bool _isPrefix;
        private readonly int _minimumLevel;
        private readonly int _maximumLevel;
        #endregion

        #region Constructors
        private ItemAffixDefinition(
            Guid id,
            Guid nameStringResourceId,
            bool isPrefix,
            int minimumLevel,
            int maximumLevel)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumLevel >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(maximumLevel >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(maximumLevel >= minimumLevel);

            _id = id;
            _nameStringResourceId = nameStringResourceId;
            _isPrefix = isPrefix;
            _minimumLevel = minimumLevel;
            _maximumLevel = maximumLevel;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid NameStringResourceId { get { return _nameStringResourceId; } }

        /// <inheritdoc />
        public bool IsPrefix { get { return _isPrefix; } }

        /// <inheritdoc />
        public int MinimumLevel { get { return _minimumLevel; } }

        /// <inheritdoc />
        public int MaximumLevel { get { return _maximumLevel; } }
        #endregion

        #region Methods
        public static IItemAffixDefinition Create(
            Guid id,
            Guid nameStringResourceId,
            bool isPrefix,
            int minimumLevel,
            int maximumLevel)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(minimumLevel >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(maximumLevel >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(maximumLevel >= minimumLevel);
            Contract.Ensures(Contract.Result<IItemAffixDefinition>() != null);

            var itemAffixDefinition = new ItemAffixDefinition(
                id,
                nameStringResourceId,
                isPrefix,
                minimumLevel,
                maximumLevel);
            return itemAffixDefinition;
        }
        #endregion
    }
}
