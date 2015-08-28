using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Application.Interface.Items
{
    public interface INamedItemAffix : IItemAffix
    {
        #region Properties
        Guid NameStringResourceId { get; }
        #endregion
    }
}
