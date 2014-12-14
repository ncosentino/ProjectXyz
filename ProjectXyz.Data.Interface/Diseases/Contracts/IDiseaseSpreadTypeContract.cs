using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ProjectXyz.Data.Interface.Diseases.Contracts
{
    [ContractClassFor(typeof(IDiseaseSpreadType))]
    public abstract class IDiseaseSpreadTypeContract : IDiseaseSpreadType
    {
        #region Properties
        public Guid Id
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                
                return default(Guid);
            }
        }

        public string Name
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                Contract.Ensures(Contract.Result<string>() != string.Empty);

                return default(string);
            }
        }
        #endregion
    }
}
