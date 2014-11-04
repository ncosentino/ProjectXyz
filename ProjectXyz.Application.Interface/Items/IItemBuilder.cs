using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IItemBuilderContract))]
    public interface IItemBuilder
    {
        #region Properties
        IMaterialFactory MaterialFactory { get; set; }
        #endregion

        #region Exposed Members
        IItemBuilder WithMaterialFactory(IMaterialFactory factory);

        IItem Build(
            IEnchantmentCalculator enchantmentCalculator,
            ProjectXyz.Data.Interface.Items.IItem itemData);
        #endregion
    }
}
