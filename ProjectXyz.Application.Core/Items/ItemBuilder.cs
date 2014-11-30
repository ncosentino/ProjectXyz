using System;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Application.Core.Items
{
    public class ItemBuilder : IItemBuilder
    {
        #region Fields
        private IMaterialFactory _materialFactory;
        #endregion

        #region Constructors
        private ItemBuilder()
        {
        }
        #endregion

        #region Properties
        public IMaterialFactory MaterialFactory
        {
            get { return _materialFactory; }
            set { _materialFactory = value; }
        }
        #endregion

        #region Methods
        public static IItemBuilder Create()
        {
            Contract.Ensures(Contract.Result<IItemBuilder>() != null);
            return new ItemBuilder();
        }

        public IItemBuilder WithMaterialFactory(IMaterialFactory factory)
        {
            _materialFactory = factory;
            return this;
        }

        public IItem Build(
            IItemContext context,
            ProjectXyz.Data.Interface.Items.IItemStore itemData)
        {
            if (_materialFactory == null)
            {
                throw new InvalidOperationException("The material factory must be set.");
            }

            return Item.Create(
                this,
                context,
                itemData);
        }
        #endregion
    }
}
