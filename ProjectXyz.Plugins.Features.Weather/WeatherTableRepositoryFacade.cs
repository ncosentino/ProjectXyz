﻿using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Weather.Api;

namespace ProjectXyz.Plugins.Features.Weather
{
    public sealed class WeatherTableRepositoryFacade : IWeatherTableRepositoryFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableWeatherTableRepository> _repositories;

        public WeatherTableRepositoryFacade(IEnumerable<IDiscoverableWeatherTableRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        public IEnumerable<IWeatherTable> GetWeatherTables(IFilterContext filterContext) =>
            _repositories.SelectMany(x => x.GetWeatherTables(filterContext));
    }
}