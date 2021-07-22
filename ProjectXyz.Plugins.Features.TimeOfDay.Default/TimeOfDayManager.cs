using System;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Enchantments.States;

namespace ProjectXyz.Plugins.Features.TimeOfDay.Default
{
    public sealed class TimeOfDayManager : ITimeOfDayManager
    {
        private readonly ITimeOfDayConverter _timeOfDayConverter;
        private readonly ITimeOfDayIdentifiers _timeOfDayIdentifiers;
        private readonly Lazy<IStateManager> _lazyStateManager;
        private double _cyclePercent;

        public TimeOfDayManager(
            ITimeOfDayConverter timeOfDayConverter,
            ITimeOfDayIdentifiers timeOfDayIdentifiers,
            Lazy<IStateManager> lazyStateManager)
        {
            _timeOfDayConverter = timeOfDayConverter;
            _timeOfDayIdentifiers = timeOfDayIdentifiers;
            _lazyStateManager = lazyStateManager;
        }

        public IIdentifier TimeOfDay => _timeOfDayConverter.GetTimeOfDay(CyclePercent);

        public double CyclePercent 
        {
            get => _cyclePercent;
            set
            {
                _cyclePercent = value;
                _lazyStateManager.Value.SetState(
                    _timeOfDayIdentifiers.TimeOfDayStateTypeId,
                    _timeOfDayIdentifiers.CyclePercentStateId,
                    value);
            }
        }
    }
}