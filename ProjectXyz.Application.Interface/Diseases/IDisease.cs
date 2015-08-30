using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Diseases
{
    public interface IDisease
    {
        #region Properties
        Guid Id { get; }

        Guid NameStringResourceId { get; }

        IDiseaseState CurrentState { get; }

        IEnumerable<IDiseaseState> States { get; }
        #endregion
    }
}
