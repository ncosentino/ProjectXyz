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
        public IMaterialFactory MaterialFactory
        {
            get
            {
                return default(IMaterialFactory);
            }

            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
            }
        }

        public IItemBuilder WithMaterialFactory(IMaterialFactory factory)
        {
            Contract.Requires<ArgumentNullException>(factory != null);
            return default(IItemBuilder);
        }

        public IItem Build(IEnchantmentCalculator enchantmentCalculator, Data.Interface.Items.IItem itemData)
        {
            Contract.Requires<ArgumentNullException>(enchantmentCalculator != null);
            Contract.Requires<ArgumentNullException>(itemData != null);
            return default(IItem);
        }
    }
}
