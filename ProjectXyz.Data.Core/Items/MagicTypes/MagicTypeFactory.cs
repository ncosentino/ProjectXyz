using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.MagicTypes;

namespace ProjectXyz.Data.Core.Items.MagicTypes
{
    public sealed class MagicTypeFactory : IMagicTypeFactory
    {
        #region Constructors
        private MagicTypeFactory()
        {
        }
        #endregion

        #region Methods
        public static IMagicTypeFactory Create()
        {
            var factory = new MagicTypeFactory();
            return factory;
        }

        public IMagicType Create(
            Guid id,
            Guid nameStringResourceId,
            int rarityWeighting)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(rarityWeighting >= 0);
            Contract.Ensures(Contract.Result<IMagicType>() != null);
            
            var magicType = MagicType.Create(
                id,
                nameStringResourceId,
                rarityWeighting);
            return magicType;
        }
        #endregion
    }
}
