using System;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Data.Core.Items.Names
{
    public sealed class ItemNamePart : IItemNamePart
    {
        #region Fields
        private readonly Guid _id;
        private readonly Guid _partId;
        private readonly Guid _nameStringResourceId;
        private readonly int _order;
        #endregion

        #region Constructors
        private ItemNamePart(
            Guid id,
            Guid partId,
            Guid nameStringResourceId,
            int order)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(partId != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(order >= 0);

            _id = id;
            _partId = partId;
            _nameStringResourceId = nameStringResourceId;
            _order = order;
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid Id { get { return _id; } }

        /// <inheritdoc />
        public Guid PartId { get { return _partId; } }

        /// <inheritdoc />
        public Guid NameStringResourceId { get { return _nameStringResourceId; } }

        /// <inheritdoc />
        public int Order { get { return _order; } }
        #endregion

        #region Methods
        public static IItemNamePart Create(
            Guid id,
            Guid partId,
            Guid nameStringResourceId,
            int order)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(partId != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(order >= 0);
            Contract.Ensures(Contract.Result<IItemNamePart>() != null);
            
            var itemNamePart = new ItemNamePart(
                id,
                partId,
                nameStringResourceId,
                order);
            return itemNamePart;
        }
        #endregion
    }
}
