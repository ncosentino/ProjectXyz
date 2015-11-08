using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Core.Interactions;
using ProjectXyz.Application.Interface.GameObjects.Actors;
using ProjectXyz.Application.Interface.Interactions;

namespace ProjectXyz.Application.Core.GameObjects.Doors
{
    public class Door : IMutableDoor
    {
        #region Fields
        private readonly IInteraction _openDoorInteraction;
        private readonly IInteraction _closeDoorInteraction;
        
        private bool _openState;
        private string _resourcePath;
        #endregion

        #region Constructors
        private Door(string resourcepath, bool opened)
        {
            _openDoorInteraction = OpenDoorInteraction.Create(this);
            _closeDoorInteraction = CloseDoorInteraction.Create(this);

            _openState = opened;
            _resourcePath = resourcepath;
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> OpenChanged;

        public event EventHandler<EventArgs> ResourcePathChanged;
        #endregion

        #region Properties
        public bool IsOpen
        {
            get
            {
                return _openState;
            }

            private set
            {
                if (_openState == value)
                {
                    return;
                }

                _openState = value;
                OnOpenChanged();
            }
        }

        public string ResourcePath
        {
            get
            {
                return _resourcePath;
            }

            set
            {
                if (_resourcePath == value)
                {
                    return;
                }

                _resourcePath = value;
                OnResourcePathChanged();
            }
        }
        #endregion

        #region Methods
        public static IMutableDoor Create(string resourcepath, bool opened)
        {
            Contract.Ensures(Contract.Result<IMutableDoor>() != null);

            return new Door(resourcepath, opened);
        }

        public IEnumerable<IInteraction> GetPossibleInteractions(IInteractionContext interactionContext, IActor actor)
        {
            if (_openDoorInteraction.CanInteract(interactionContext, actor))
            {
                yield return _openDoorInteraction;
            }

            if (_closeDoorInteraction.CanInteract(interactionContext, actor))
            {
                yield return _closeDoorInteraction;
            }
        }

        public void Open()
        {
            IsOpen = true;
        }   

        public void Close()
        {
            IsOpen = false;
        }

        private void OnOpenChanged()
        {
            var handler = OpenChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnResourcePathChanged()
        {
            var handler = ResourcePathChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
