using System;

namespace ProjectXyz.Game.Core.Autofac
{
    public sealed class AssemblyLoadFailedEventArgs : EventArgs
    {
        public AssemblyLoadFailedEventArgs(
            string assemblyFilePath,
            Exception exception)
        {
            AssemblyFilePath = assemblyFilePath;
            Exception = exception;
        }

        public string AssemblyFilePath { get; }

        public Exception Exception { get; }

        public bool Handled { get; set; }
    }
}