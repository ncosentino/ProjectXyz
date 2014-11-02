using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Items.Contracts;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Interface.Items
{
    [ContractClass(typeof(IItemContract))]
    public interface IItem : IGameObject
    {
        #region Properties
        string Name { get; set; }

        string MagicType { get; set; }

        IMutableStatCollection<IMutableStat> Stats { get; }

        IMutableEnchantmentCollection Enchantments { get; }

        IRequirements Requirements { get; }

        IMutableItemCollection SocketedItems { get; }
        #endregion
    }
}
