using System;

using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public class PlayerControlledBehavior :
        BaseBehavior,
        IPlayerControlledBehavior
    {
        private bool _isActive;

        public event EventHandler<EventArgs> IsActiveChanged;

        public bool IsActive 
        {
            get => _isActive; 
            set
            {
                if (_isActive == value)
                {
                    return;
                }

                _isActive = value;
                IsActiveChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
