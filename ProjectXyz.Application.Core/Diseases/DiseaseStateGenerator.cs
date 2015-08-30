using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Application.Interface.Diseases;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface;
using ProjectXyz.Data.Interface.Diseases;

namespace ProjectXyz.Application.Core.Diseases
{
    public sealed class DiseaseStateGenerator : IDiseaseStateGenerator
    {
        #region Fields
        private readonly IDiseaseStateFactory _diseaseStateFactory;
        private readonly IEnchantmentGenerator _enchantmentGenerator;
        private readonly IDiseaseStateDefinitionRepository _diseaseStateDefinitionRepository;
        private readonly IDiseaseStatesEnchantmentsRepository _diseaseStateEnchantmentsRepository;
        #endregion

        #region Constructors
        private DiseaseStateGenerator(
            IDiseaseStateFactory diseaseStateFactory,
            IEnchantmentGenerator enchantmentGenerator,
            IDiseaseStateDefinitionRepository diseaseStateDefinitionRepository,
            IDiseaseStatesEnchantmentsRepository diseaseStateEnchantmentsRepository)
        {
            Contract.Requires<ArgumentNullException>(diseaseStateFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentGenerator != null);
            Contract.Requires<ArgumentNullException>(diseaseStateDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(diseaseStateEnchantmentsRepository != null);

            _diseaseStateFactory = diseaseStateFactory;
            _enchantmentGenerator = enchantmentGenerator;
            _diseaseStateDefinitionRepository = diseaseStateDefinitionRepository;
            _diseaseStateEnchantmentsRepository = diseaseStateEnchantmentsRepository;
        }
        #endregion

        #region Methods
        public static IDiseaseStateGenerator Create(
            IDiseaseStateFactory diseaseStateFactory,
            IEnchantmentGenerator enchantmentGenerator,
            IDiseaseStateDefinitionRepository diseaseStateDefinitionRepository,
            IDiseaseStatesEnchantmentsRepository diseaseStateEnchantmentsRepository)
        {
            Contract.Requires<ArgumentNullException>(diseaseStateFactory != null);
            Contract.Requires<ArgumentNullException>(enchantmentGenerator != null);
            Contract.Requires<ArgumentNullException>(diseaseStateDefinitionRepository != null);
            Contract.Requires<ArgumentNullException>(diseaseStateEnchantmentsRepository != null);
            Contract.Ensures(Contract.Result<IDiseaseStateGenerator>() != null);

            return new DiseaseStateGenerator(
                diseaseStateFactory,
                enchantmentGenerator,
                diseaseStateDefinitionRepository,
                diseaseStateEnchantmentsRepository);
        }

        public IDiseaseState GenerateForId(IRandom randomizer, Guid id)
        {
            var diseaseStateDefinition = _diseaseStateDefinitionRepository.GetById(id);
            var diseaseStateEnchantments = _diseaseStateEnchantmentsRepository.GetByDiseaseStateId(id);
            var enchantments = GetEnchantments(
                _enchantmentGenerator,
                randomizer,
                diseaseStateEnchantments.EnchantmentIds);            

            return _diseaseStateFactory.Create(
                diseaseStateDefinition.Id,
                diseaseStateDefinition.NameStringResourceId,
                diseaseStateDefinition.PreviousStateId,
                diseaseStateDefinition.NextStateId,
                diseaseStateDefinition.DiseaseSpreadTypeId,
                enchantments);
        }

        private IEnumerable<IEnchantment> GetEnchantments(
            IEnchantmentGenerator enchantmentGenerator, 
            IRandom randomizer, 
            IEnumerable<Guid> enchantmentIds)
        {
            Contract.Requires<ArgumentNullException>(enchantmentGenerator != null);
            Contract.Requires<ArgumentNullException>(randomizer != null);
            Contract.Requires<ArgumentNullException>(enchantmentIds != null);
            Contract.Ensures(Contract.Result<IEnumerable<IEnchantment>>() != null);

            foreach (var enchantmentId in enchantmentIds)
            {
                yield return enchantmentGenerator.Generate(randomizer, enchantmentId);
            }
        }
        #endregion
    }
}
