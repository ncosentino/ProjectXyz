using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Application.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Diseases
{
    public interface IDiseaseState
    {
        #region Properties
        Guid Id { get; }

        string Name { get; }

        IEnumerable<IEnchantment> Enchantments { get; }

        Guid SpreadType { get; }

        Guid PreviousStateId { get; }
        
        Guid NextStateId { get; }
        #endregion
    }
}
