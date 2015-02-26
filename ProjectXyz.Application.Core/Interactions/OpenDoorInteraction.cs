using ProjectXyz.Application.Core.GameObjects;
using ProjectXyz.Application.Core.GameObjects.Doors;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Interactions;

namespace ProjectXyz.Application.Core.Interactions
{
    public class OpenDoorInteraction : IInteraction
    {
        #region Fields
        private readonly Door _door;
        #endregion

        #region Constructors
        private OpenDoorInteraction(Door door)
        {
            _door = door;
        }
        #endregion

        #region Methods
        public static IInteraction Create(Door door)
        {
            return new OpenDoorInteraction(door);
        }

        public void Interact(IInteractionContext interactionContext, IActor actor)
        {
            _door.Open();
        }

        public bool CanInteract(IInteractionContext interactionContext, IActor actor)
        {
            return !_door.IsOpen;
        }
        #endregion
    }
}
