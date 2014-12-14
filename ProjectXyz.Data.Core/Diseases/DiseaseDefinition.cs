using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using ProjectXyz.Data.Interface.Diseases;

namespace ProjectXyz.Data.Core.Diseases
{
    public sealed class DiseaseDefinition : IDiseaseDefinition
    {
        #region Fields
        private readonly Guid _id;
        private readonly string _name;
        private readonly Guid _diseaseStateId;
        #endregion

        #region Constructors
        private DiseaseDefinition(
            Guid id,
            string name,
            Guid diseaseStateId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);
            Contract.Requires<ArgumentException>(diseaseStateId != Guid.Empty);

            _id = id;
            _name = name;
            _diseaseStateId = diseaseStateId;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public Guid DiseaseStatesId
        {
            get { return _diseaseStateId; }
        }
        #endregion

        #region Methods
        public static IDiseaseDefinition Create(
            Guid id,
            string name,
            Guid diseaseStateId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentException>(name != string.Empty);
            Contract.Requires<ArgumentException>(diseaseStateId != Guid.Empty);
            Contract.Ensures(Contract.Result<IDiseaseDefinition>() != null);

            return new DiseaseDefinition(
                id,
                name,
                diseaseStateId);
        }
        #endregion
    }
}
