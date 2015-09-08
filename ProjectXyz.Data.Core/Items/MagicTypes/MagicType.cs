using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.MagicTypes;

namespace ProjectXyz.Data.Core.Items.MagicTypes
{
    public sealed class MagicType : IMagicType
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _nameStringResourceId;
        private readonly int _rarityWeighting;
        #endregion

        #region Constructors
        private MagicType(
            Guid id,
            Guid nameStringResourceId,
            int rarityWeighting)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(rarityWeighting >= 0);
            
            _id = id;
            _nameStringResourceId = nameStringResourceId;
            _rarityWeighting = rarityWeighting;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid NameStringResourceId { get { return _nameStringResourceId; } }

        /// <inheritdoc />
        public int RarityWeighting { get { return _rarityWeighting; } }
        #endregion

        #region Methods
        public static IMagicType Create(
            Guid id,
            Guid nameStringResourceId,
            int rarityWeighting)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(rarityWeighting >= 0);
            Contract.Ensures(Contract.Result<IMagicType>() != null);
            
            var magicType = new MagicType(
                id,
                nameStringResourceId,
                rarityWeighting);
            return magicType;
        }
        #endregion
    }
}
