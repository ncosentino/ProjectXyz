using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectXyz.Api.Messaging.Core.CharacterCreation
{
    public sealed class ClassInfo
    {
        #region Properties
        public Guid ClassId { get; set; }

        public Guid NameStringResourceId { get; set; }
        #endregion
    }
}
