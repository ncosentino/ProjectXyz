namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public interface IStatVisitorRegistrar
    {
        void Register(IStatCalculatorHandler handler);
    }
}
