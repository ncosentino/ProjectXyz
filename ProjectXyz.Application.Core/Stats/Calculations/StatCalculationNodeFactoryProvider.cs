using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats.Calculations;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculationNodeFactoryProvider : IMutableStatCalculationNodeFactoryProvider
    {
        private readonly List<IStatCalculationNodeFactory> _factories;

        public StatCalculationNodeFactoryProvider()
        {
            _factories = new List<IStatCalculationNodeFactory>();
        }

        public IReadOnlyCollection<IStatCalculationNodeFactory> Factories
        {
            get { return _factories; }
        }

        public void Add(IStatCalculationNodeFactory statCalculationNodeFactory)
        {
            _factories.Add(statCalculationNodeFactory);
        }

        public void Remove(IStatCalculationNodeFactory statCalculationNodeFactory)
        {
            _factories.Remove(statCalculationNodeFactory);
        }
    }
}
