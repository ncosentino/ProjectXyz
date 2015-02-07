using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Maps
{
    public interface IMap
    {
        #region Properties
        Guid Id { get; }

        // FIXME: this *IS* the resource
        string ResourceName { get; }
        #endregion
    }
}
