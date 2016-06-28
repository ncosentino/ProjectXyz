using System.Collections.Generic;
using ProjectXyz.Framework.Interface;
using System;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStatCalculationNodeFactory
    {
        bool TryCreate(
            IIdentifier statDefinitionId,
            string expression,
            out IStatCalculationNode statCalculationNode);

    }

    public static class IStatCalculationNodeFactoryExtensions
    {
        public static IStatCalculationNode Create(
            this IStatCalculationNodeFactory statCalculationNodeFactory,
            IIdentifier statDefinitionId,
            string expression)
        {
            IStatCalculationNode statCalculationNode;
            if (statCalculationNodeFactory.TryCreate(
                statDefinitionId,
                expression,
                out statCalculationNode))
            {
                return statCalculationNode;
            }

            throw new InvalidOperationException("Could not create IStatCalculationNode instance.");
        }
    }
}