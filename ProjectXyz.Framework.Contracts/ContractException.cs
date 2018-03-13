using System;

namespace ProjectXyz.Framework.Contracts
{
    public sealed class ContractException : Exception
    {
        public ContractException(string message)
            : base(message)
        {
        }
    }
}