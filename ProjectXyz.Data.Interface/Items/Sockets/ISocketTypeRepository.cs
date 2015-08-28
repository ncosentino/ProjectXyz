using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Sockets
{
    public interface ISocketTypeRepository
    {
        #region Methods
        void Add(ISocketType socketType);

        void RemoveById(Guid id);


        ISocketType GetById(Guid id);

        IEnumerable<ISocketType> GetAll();
        #endregion
    }
}
