using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Logging;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default
{
    public sealed class FilterComponentToBehaviorConverterFacade : IFilterComponentToBehaviorConverterFacade
    {
        private readonly Dictionary<Type, ConvertFilterComponentToBehaviorDelegate> _mapping;
        private readonly List<Tuple<Predicate<IFilterComponent>, ConvertFilterComponentToBehaviorDelegate>> _predicateMapping;

        public FilterComponentToBehaviorConverterFacade(
            ILogger logger,
            IEnumerable<IDiscoverableFilterComponentToBehaviorConverter> filterComponentToBehaviorConverters,
            IEnumerable<IDiscoverablePredicateFilterComponentToBehaviorConverter> predicateFilterComponentToBehaviorConverters)
        {
            _mapping = new Dictionary<Type, ConvertFilterComponentToBehaviorDelegate>();
            _predicateMapping = new List<Tuple<Predicate<IFilterComponent>, ConvertFilterComponentToBehaviorDelegate>>();

            logger.Debug($"Registering converters to '{this}'...");

            foreach (var converter in filterComponentToBehaviorConverters)
            {
                logger.Debug(
                    $"Registering '{converter}' on type '{converter.ComponentType}' " +
                    $"to '{this}'.");
                Register(
                    converter.ComponentType,
                    converter.Convert);
            }

            foreach (var converter in predicateFilterComponentToBehaviorConverters)
            {
                logger.Debug($"Registering predicate-based '{converter}' to '{this}'.");
                Register(
                    converter.CanConvert,
                    converter.Convert);
            }

            logger.Debug($"Done registering converters to '{this}'.");
        }

        public void Register<T>(ConvertFilterComponentToBehaviorDelegate callback)
        {
            Register(typeof(T), callback);
        }

        public void Register(
            Type t,
            ConvertFilterComponentToBehaviorDelegate callback)
        {
            _mapping.Add(t, callback);
        }

        public void Register(
            Predicate<IFilterComponent> predicate,
            ConvertFilterComponentToBehaviorDelegate callback)
        {
            _predicateMapping.Add(Tuple.Create(predicate, callback));
        }

        public IEnumerable<IBehavior> Convert(IFilterComponent filterComponent)
        {
            if (!_mapping.TryGetValue(
                filterComponent.GetType(),
                out var convertCallback))
            {
                convertCallback = _predicateMapping
                    .FirstOrDefault(x => x.Item1(filterComponent))
                    ?.Item2;
                if (convertCallback == null)
                {
                    throw new InvalidOperationException(
                        "There was no registered mapping to convert " +
                        $"'{filterComponent.GetType()}'.");
                }
            }

            var converted = convertCallback.Invoke(filterComponent);
            return converted;
        }
    }
}