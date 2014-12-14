using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Diseases;

namespace ProjectXyz.Data.Core.Diseases
{
    public sealed class DiseaseSpreadTypeFactory : IDiseaseSpreadTypeFactory
    {
        #region Constructors
        private DiseaseSpreadTypeFactory()
        {
        }
        #endregion

        #region Methods
        public static IDiseaseSpreadTypeFactory Create()
        {
            Contract.Ensures(Contract.Result<IDiseaseSpreadTypeFactory>() != null);

            return new DiseaseSpreadTypeFactory();
        }

        public IDiseaseSpreadType Create(Guid id, string name)
        {
            return DiseaseSpreadType.Create(id, name);
        }
        #endregion
    }
}
