using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Application.Interface.Items.Contracts
{
    [ContractClassFor(typeof(IItemBuilder))]
    public abstract class IItemBuilderContract : IItemBuilder
    {
        #region Properties
        public IMaterialFactory MaterialFactory
        {
            get
            {
                Contract.Ensures(Contract.Result<IMaterialFactory>() != null);
                return default(IMaterialFactory);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
            }
        }
        #endregion

        #region Exposed Members
        public IItemBuilder WithMaterialFactory(IMaterialFactory factory)
        {
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemBuilder>() != null);
            return default(IItemBuilder);
        }

        public IItem Build(IItemContext context, Data.Interface.Items.IItem itemData)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(itemData != null);
            Contract.Ensures(Contract.Result<IItem>() != null);
            return default(IItem);
        }
        #endregion
    }
}
