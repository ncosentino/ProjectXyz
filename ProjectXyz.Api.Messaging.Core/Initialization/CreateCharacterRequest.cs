using System;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.Initialization
{
    public sealed class CreateCharacterRequest : BaseRequest
    {
        #region Properties
        public string Name { get; set; }

        public bool Male { get; set; }

        public Guid RaceId { get; set; }

        public Guid ClassId { get; set; }
        #endregion
    }
}
