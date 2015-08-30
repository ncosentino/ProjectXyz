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
        private readonly Guid _nameStringResourceId;
        private readonly Guid _diseaseStateId;
        #endregion

        #region Constructors
        private DiseaseDefinition(
            Guid id,
            Guid nameStringResourceId,
            Guid diseaseStateId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(diseaseStateId != Guid.Empty);

            _id = id;
            _nameStringResourceId = nameStringResourceId;
            _diseaseStateId = diseaseStateId;
        }
        #endregion

        #region Properties
        public Guid Id
        {
            get { return _id; }
        }

        public Guid NameStringResourceId
        {
            get { return _nameStringResourceId; }
        }

        public Guid DiseaseStatesId
        {
            get { return _diseaseStateId; }
        }
        #endregion

        #region Methods
        public static IDiseaseDefinition Create(
            Guid id,
            Guid nameStringResourceId,
            Guid diseaseStateId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(nameStringResourceId != Guid.Empty);
            Contract.Requires<ArgumentException>(diseaseStateId != Guid.Empty);
            Contract.Ensures(Contract.Result<IDiseaseDefinition>() != null);

            return new DiseaseDefinition(
                id,
                nameStringResourceId,
                diseaseStateId);
        }
        #endregion
    }
}
