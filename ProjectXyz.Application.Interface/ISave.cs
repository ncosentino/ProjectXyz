using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Application.Interface
{
    public interface ISave<TSource, TDestination>
    {
        #region Methods
        TDestination Save(TSource source);
        #endregion
    }
}
