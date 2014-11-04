using System;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Application.Core.Items
{
    public partial class Item
    {
        public class Builder : IItemBuilder
        {
            #region Fields
            private IMaterialFactory _materialFactory;
            #endregion

            #region Constructors
            private Builder()
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

            #region Exposed Members
            public static IItemBuilder Create()
            {
                return new Builder();
            }

            public IItemBuilder WithMaterialFactory(IMaterialFactory factory)
            {
                _materialFactory = factory;
                return this;
            }

            public IItem Build(
                IEnchantmentCalculator enchantmentCalculator, 
                ProjectXyz.Data.Interface.Items.IItem itemData)
            {
                if (_materialFactory == null)
                {
                    throw new InvalidOperationException("The material factory must be set.");
                }

                return Item.Create(
                    this, 
                    enchantmentCalculator, 
                    itemData);
            }
            #endregion
        }
    }
}
