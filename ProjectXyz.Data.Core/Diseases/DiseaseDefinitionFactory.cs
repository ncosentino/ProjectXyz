using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Diseases;

namespace ProjectXyz.Data.Core.Diseases
{
    public sealed class DiseaseDefinitionFactory : IDiseaseDefinitionFactory
    {
        #region Constructors
        private DiseaseDefinitionFactory()
        {
        }
        #endregion

        #region Methods
        public static IDiseaseDefinitionFactory Create()
        {
            Contract.Ensures(Contract.Result<IDiseaseDefinitionFactory>() != null);

            return new DiseaseDefinitionFactory();
        }

        public IDiseaseDefinition Create(Guid id, string name, Guid diseaseStatesId)
        {
            return DiseaseDefinition.Create(id, name, diseaseStatesId);
        }
        #endregion
    }
}
