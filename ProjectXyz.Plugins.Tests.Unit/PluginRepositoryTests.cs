using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ProjectXyz.Plugins.Tests.Unit
{
    public class PluginRepositoryTests
    {
        #region Methods
        [Fact]
        private void Plugins_SingleParameterlessPlugin_OnePluginInstance()
        {
            // Setup
            var pluginDirectoryPaths = new[]
            {
                "path to directory",
            };

            var assembly = Assembly.GetExecutingAssembly();

            Func<string, IEnumerable<string>> getPluginCandidateFilesCallback = directory =>
            {
                Assert.Equal("path to directory", directory);
                return new [] { "plugin file path" };
            };

            Func<string, Assembly> createAssemblyCallback = candidatePluginFile =>
            {
                Assert.Equal("plugin file path", candidatePluginFile);
                return assembly;
            };

            var pluginInstance = new ParameterlessPlugin();
            var candidateCount = 0;

            Func<Assembly, Type, ParameterlessPlugin> createPluginCallback = (asmbly, pluginType) =>
            {
                Assert.Equal(assembly, asmbly);
                Assert.Equal(typeof(ParameterlessPlugin), pluginType);
                candidateCount++;

                return pluginInstance;
            };

            var pluginRepository = PluginRepository<ParameterlessPlugin>.Create(
                getPluginCandidateFilesCallback,
                createAssemblyCallback,
                createPluginCallback,
                pluginDirectoryPaths);

            // Execute
            var result = pluginRepository.Plugins.ToList();

            // Assert
            Assert.Equal(1, result.Count);
            Assert.Equal(pluginInstance, result.First());
            Assert.Equal(1, candidateCount);
        }
        #endregion

        #region Classes
        public class ParameterlessPlugin
        {
        }
        #endregion
    }
}
