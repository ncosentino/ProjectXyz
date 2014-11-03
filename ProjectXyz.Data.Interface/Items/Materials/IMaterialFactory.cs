using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Items.Materials
{
    public interface IMaterialFactory
    {
        #region Methods
        IMaterial Load(string materialType);
        #endregion
    }
}
