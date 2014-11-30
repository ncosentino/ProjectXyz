using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface.Items
{
    public interface INamedItemAffix : IItemAffix
    {
        #region Properties
        string Name { get; }
        #endregion
    }
}
