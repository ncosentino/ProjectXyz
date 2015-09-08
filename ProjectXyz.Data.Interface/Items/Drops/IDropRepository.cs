using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Drops
{
    public interface IDropRepository
    {
        #region Methods
        IDrop Add(
            Guid id,
            int minimum,
            int maximum,
            bool canRepeat);

        IDrop GetById(Guid id);

        IEnumerable<IDrop> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
