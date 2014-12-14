using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Diseases.Contracts
{
    [ContractClassFor(typeof(IDiseaseSpreadTypeFactory))]
    public abstract class IDiseaseSpreadTypeFactoryContract : IDiseaseSpreadTypeFactory
    {
        #region Methods
        public IDiseaseSpreadType Create(Guid id, string name)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);
            Contract.Ensures(Contract.Result<IDiseaseSpreadType>() != null);

            return default(IDiseaseSpreadType);
        }
        #endregion
    }
}
