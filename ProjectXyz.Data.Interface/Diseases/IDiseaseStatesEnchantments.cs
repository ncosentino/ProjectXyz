using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Diseases
{
    public interface IDiseaseStatesEnchantments
    {
        #region Properties
        Guid Id { get; }

        Guid DiseaseStateId { get; }

        IEnumerable<Guid> EnchantmentIds { get; }
        #endregion
    }
}
