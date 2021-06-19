using System;

using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.PartyManagement.Default
{

    public sealed class RosterBehavior :
        BaseBehavior,
        IRosterBehavior
    {
        private bool _isActiveParty;
        private bool _isPartyLeader;

        public event EventHandler IsPartyLeaderChanged;

        public event EventHandler IsActivePartyChanged;

        public bool IsPartyLeader
        {
            get => _isPartyLeader;
            set
            {
                if (_isPartyLeader == value)
                {
                    return;
                }

                // cannot be a party leader if we're not in the active party,
                // so enforce this here
                if (value)
                {
                    IsActiveParty = true;
                }

                _isPartyLeader = value;
                IsPartyLeaderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsActiveParty
        {
            get => _isActiveParty;
            set
            {
                if (_isActiveParty == value)
                {
                    return;
                }

                // cannot be a party leader if we're not in the active party,
                // so enforce this here
                if (!value)
                {
                    IsPartyLeader = false;
                }

                _isActiveParty = value;
                IsActivePartyChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
