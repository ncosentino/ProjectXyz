using System;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Core.CharacterCreation;
using ProjectXyz.Api.Messaging.Core.General;
using ProjectXyz.Api.Messaging.Core.Initialization;
using ProjectXyz.Application.Core.Maps;
using ProjectXyz.Application.Core.Time;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Application.Interface.Worlds;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Game.Core.Binding
{
    public sealed class CharacterCreationBinding : IApiBinder
    {
        #region Fields
        private readonly IApiManager _apiManager;
        private readonly IDataManager _dataManager;
        #endregion

        #region Constructors
        private CharacterCreationBinding(
            IApiManager apiManager,
            IDataManager dataManager)
        {
            _apiManager = apiManager;
            _dataManager = dataManager;
            Subscribe();
        }

        ~CharacterCreationBinding()
        {
            Dispose(false);
        }
        #endregion

        #region Methods
        public static IApiBinder Create(
            IApiManager apiManager,
            IDataManager dataManager)
        {
            var binder = new CharacterCreationBinding(
                apiManager,
                dataManager);
            return binder;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            Unsubscribe();
        }

        private void Subscribe()
        {
            _apiManager.RequestRegistrar.Subscribe<CreateCharacterRequest>(HandleCreateCharacterRequest);
            _apiManager.RequestRegistrar.Subscribe<GetNewCharacterOptionsRequest>(HandleGetNewCharacterOptionsRequest);
        }

        private void Unsubscribe()
        {
            _apiManager.RequestRegistrar.Unsubscribe<CreateCharacterRequest>(HandleCreateCharacterRequest);
            _apiManager.RequestRegistrar.Unsubscribe<GetNewCharacterOptionsRequest>(HandleGetNewCharacterOptionsRequest);
        }

        private void HandleCreateCharacterRequest(CreateCharacterRequest request)
        {
            // TODO: actually make a character and all that fun stuff

            _apiManager.Responder.Respond<BooleanResultResponse>(request.Id, r =>
            {
                r.Result = true;
            });
        }

        private void HandleGetNewCharacterOptionsRequest(GetNewCharacterOptionsRequest request)
        {
            // TODO: actually get all the options
            var classes = new[]
            {
                new ClassInfo() { ClassId = Guid.NewGuid(), NameStringResourceId = Guid.NewGuid()},
                new ClassInfo() { ClassId = Guid.NewGuid(), NameStringResourceId = Guid.NewGuid()},
            };

            var races = new[]
            {
                new RaceInfo() { RaceId = Guid.NewGuid(), NameStringResourceId = Guid.NewGuid()},
                new RaceInfo() { RaceId = Guid.NewGuid(), NameStringResourceId = Guid.NewGuid()},
            };

            var genders = new[]
            {
                new GenderInfo() { GenderId = Guid.NewGuid(), NameStringResourceId = Guid.NewGuid() },
                new GenderInfo() { GenderId = Guid.NewGuid(), NameStringResourceId = Guid.NewGuid() },
            };

            _apiManager.Responder.Respond<GetNewCharacterOptionsResponse>(request.Id, r =>
            {
                r.Classes = classes;
                r.Races = races;
                r.Genders = genders;
                r.MaximumCharacterNameLength = 25;
                r.MinimumCharacterNameLength = 2;
            });
        }
        #endregion
    }
}