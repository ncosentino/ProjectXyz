namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public interface IGetEnchantmentsHandler
    {
        GetEnchantmentsDelegate GetEnchantments { get; }

        CanGetEnchantmentsDelegate CanGetEnchantments { get; }
    }
}
