using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Diseases
{
    public interface IDiseaseStateGenerator
    {
        #region Methods
        IDiseaseState GenerateForId(IRandom randomizer, Guid id);
        #endregion
    }
}
