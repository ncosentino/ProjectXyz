namespace ProjectXyz.Plugins.Features.TimeOfDay.Default
{
    /// <inheritdoc/>
    /// <remarks>
    /// Please note that this interface lives in the default implementation and 
    /// not the API as to only allow code in this domain to change the cycle 
    /// percent... I could totally envision situations where something may 
    /// want to explicitly set the time of day, and maybe then we need to move 
    /// this into the API.
    /// </remarks>
    public interface ITimeOfDayManager : IReadOnlyTimeOfDayManager
    {
        new double CyclePercent { get; set; }
    }
}