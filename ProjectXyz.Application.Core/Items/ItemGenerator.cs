using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Core.Items
{
    public sealed class ItemGenerator : IItemGenerator
    {
        #region Fields
        private readonly Dictionary<Guid, GenerateItemDelegate> _generateItemDelegates;
        #endregion

        #region Constructors
        private ItemGenerator()
        {
            _generateItemDelegates = new Dictionary<Guid, GenerateItemDelegate>();
        }
        #endregion

        #region Methods
        public static IItemGenerator Create()
        {
            var generator = new ItemGenerator();
            return generator;
        }

        /// <inheritdoc />
        public void RegisterCallback(Guid magicTypeId, GenerateItemDelegate callbackToRegister)
        {
            _generateItemDelegates[magicTypeId] = callbackToRegister;
        }
        
        /// <inheritdoc />
        public IItem Generate(
            IRandom randomizer,
            Guid itemDefinitionId,
            Guid magicTypeId,
            int level,
            IItemContext itemContext)
        {
            if (!_generateItemDelegates.ContainsKey(magicTypeId))
            {
                throw new InvalidOperationException(string.Format("No callback registered for magic type id '{0}'.", magicTypeId));
            }

            var callback = _generateItemDelegates[magicTypeId];
            var result = callback.Invoke(
                randomizer, 
                itemDefinitionId,
                level,
                itemContext);
            return result;
        }
        #endregion
    }
}
