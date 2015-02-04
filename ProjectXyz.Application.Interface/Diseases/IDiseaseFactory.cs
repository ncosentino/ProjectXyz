using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Diseases
{
    public interface IDiseaseFactory
    {
        #region Methods
        IDisease Create(Guid id, string name, IEnumerable<IDiseaseState> states);
        #endregion
    }
}
