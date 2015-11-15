using System;
using System.Collections.Generic;

namespace ProjectXyz.Api.Messaging.Core.CharacterCreation
{
    public sealed class GetNewCharacterOptionsResponse : BaseResponse
    {
        #region Properties
        public int MinimumCharacterNameLength { get; set; }

        public int MaximumCharacterNameLength { get; set; }

        public IEnumerable<RaceInfo> Races { get; set; }

        public IEnumerable<ClassInfo> Classes { get; set; }

        public IEnumerable<GenderInfo> Genders { get; set; }
        #endregion
    }
}
