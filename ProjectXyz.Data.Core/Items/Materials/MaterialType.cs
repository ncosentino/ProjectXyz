using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Data.Core.Items.Materials
{
    public sealed class MaterialType : IMaterialType
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _nameStringResourceId;
        #endregion

        #region Constructors
        private MaterialType(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            
            _id = id;
            _nameStringResourceId = nameStringResourceId;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid NameStringResourceId { get { return _nameStringResourceId; } }
        #endregion

        #region Methods
        public static IMaterialType Create(
            Guid id,
            Guid nameStringResourceId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Ensures(Contract.Result<IMaterialType>() != null);
            
            var materialType = new MaterialType(
                id,
                nameStringResourceId);
            return materialType;
        }
        #endregion
    }
}
