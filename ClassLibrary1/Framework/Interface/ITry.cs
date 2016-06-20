using System;

namespace ClassLibrary1.Framework.Interface
{
    public interface ITry
    {
        Exception Dangerous(Action callback);
    }
}