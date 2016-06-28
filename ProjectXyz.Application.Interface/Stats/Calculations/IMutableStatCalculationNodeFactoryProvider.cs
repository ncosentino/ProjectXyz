namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IMutableStatCalculationNodeFactoryProvider : IStatCalculationNodeFactoryProvider
    {
        void Add(IStatCalculationNodeFactory statCalculationNodeFactory);

        void Remove(IStatCalculationNodeFactory statCalculationNodeFactory);
    }
}
