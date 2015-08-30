using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Diseases;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Diseases;

namespace ProjectXyz.Application.Core.Diseases
{
    public sealed class DiseaseGenerator : IDiseaseGenerator
    {
        #region Fields
        private readonly IDiseaseFactory _diseaseFactory;
        private readonly IDiseaseStateGenerator _diseaseStateGenerator;
        private readonly IDiseaseDefinitionRepository _diseaseDefinitionRepository;
        #endregion

        #region Constructors
        private DiseaseGenerator(
            IDiseaseFactory diseaseFactory,
            IDiseaseStateGenerator diseaseStateGenerator,
            IDiseaseDefinitionRepository diseaseDefinitionRepository)
        {
            Contract.Requires<ArgumentNullException>(diseaseFactory != null);
            Contract.Requires<ArgumentNullException>(diseaseStateGenerator != null);
            Contract.Requires<ArgumentNullException>(diseaseDefinitionRepository != null);

            _diseaseFactory = diseaseFactory;
            _diseaseStateGenerator = diseaseStateGenerator;
            _diseaseDefinitionRepository = diseaseDefinitionRepository;
        }
        #endregion

        #region Methods
        public static IDiseaseGenerator Create(
            IDiseaseFactory diseaseFactory,
            IDiseaseStateGenerator diseaseStateGenerator,
            IDiseaseDefinitionRepository diseaseDefinitionRepository)
        {
            Contract.Requires<ArgumentNullException>(diseaseFactory != null);
            Contract.Requires<ArgumentNullException>(diseaseStateGenerator != null);
            Contract.Requires<ArgumentNullException>(diseaseDefinitionRepository != null);
            Contract.Ensures(Contract.Result<IDiseaseGenerator>() != null);

            return new DiseaseGenerator(
                diseaseFactory,
                diseaseStateGenerator,
                diseaseDefinitionRepository);
        }

        public IDisease GenerateForId(IRandom randomizer, Guid id)
        {
            var diseaseDefinition = _diseaseDefinitionRepository.GetById(id);
            var diseaseStates = GetDiseaseStates(
                randomizer,
                diseaseDefinition.DiseaseStatesId);

            return _diseaseFactory.Create(
                id, 
                diseaseDefinition.NameStringResourceId,
                diseaseStates);
        }

        private IEnumerable<IDiseaseState> GetDiseaseStates(IRandom randomizer, Guid firstStateId)
        {
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Ensures(Contract.Result<IEnumerable<IDiseaseState>>() != null);

            var diseaseStateIds = new HashSet<Guid>();
            var nextDiseaseStateId = firstStateId;
            
            while (
                !diseaseStateIds.Contains(nextDiseaseStateId) && 
                nextDiseaseStateId != Guid.Empty)
            {
                var diseaseState = _diseaseStateGenerator.GenerateForId(
                    randomizer,
                    nextDiseaseStateId);

                nextDiseaseStateId = diseaseState.NextStateId;

                yield return diseaseState;
            }
        }
        #endregion
    }
}
