using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectXyz.Api.Messaging.Core.CharacterCreation
{
    public sealed class GenderInfo
    {
        #region Properties
        public Guid GenderId { get; set; }

        public Guid NameStringResourceId { get; set; }
        #endregion
    }
}
