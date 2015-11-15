using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectXyz.Api.Messaging.Core.CharacterCreation
{
    public sealed class RaceInfo
    {
        #region Properties
        public Guid RaceId { get; set; }

        public Guid NameStringResourceId { get; set; }
        #endregion
    }
}
