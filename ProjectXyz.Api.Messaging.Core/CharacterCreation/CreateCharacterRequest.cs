using System;

namespace ProjectXyz.Api.Messaging.Core.CharacterCreation
{
    public sealed class CreateCharacterRequest : BaseRequest
    {
        #region Properties
        public string Name { get; set; }

        public Guid GenderId { get; set; }

        public Guid RaceId { get; set; }

        public Guid ClassId { get; set; }
        #endregion
    }
}
