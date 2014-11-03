using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Items.Materials
{
    public interface IMaterial
    {
        #region Properties
        string Name { get; }

        string MaterialType { get; }
        #endregion
    }
}
