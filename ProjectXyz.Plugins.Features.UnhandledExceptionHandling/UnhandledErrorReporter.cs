using System;
using ProjectXyz.Api.Logging;

namespace ProjectXyz.Plugins.Features.UnhandledExceptionHandling
{
    public sealed class UnhandledErrorReporter
    {
        private readonly ILogger _logger;

        public UnhandledErrorReporter(ILogger logger)
        {
            _logger = logger;
        }

        public void Report(Exception exception)
        {
            _logger.Error("An unhandled exception was encountered.", exception);
        }
    }
}