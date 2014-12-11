using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Diseases
{
    public interface IDisease
    {
        #region Properties
        Guid Id { get; }

        string Name { get; }

        IDiseaseState CurrentState { get; }

        IEnumerable<IDiseaseState> States { get; }
        #endregion
    }
}
