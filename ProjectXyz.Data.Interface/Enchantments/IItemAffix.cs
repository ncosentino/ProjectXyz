using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IItemAffix
    {
        #region Properties
        Guid Id { get; }

        string Name { get; }

        bool IsPrefix { get;}

        bool Magic { get; }

        bool Rare { get; }

        int MinimumLevel { get; }
        
        int MaximumLevel { get; }

        Guid AffixEnchantmentsId { get; }
        #endregion
    }
}
