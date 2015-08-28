using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Data.Core.Items.Materials
{
    public sealed class MaterialTypeFactory : IMaterialTypeFactory
    {
        #region Constructors
        private MaterialTypeFactory()
        {
        }
        #endregion

        #region Methods
        public static IMaterialTypeFactory Create()
        {
            var factory = new MaterialTypeFactory();
            return factory;
        }

        public IMaterialType Create(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IMaterialType>() != null);
            
            var materialType = MaterialType.Create(
                id,
                nameStringResourceId);
            return materialType;
        }
        #endregion
    }
}
