using System;

namespace ProjectXyz.Framework.ViewWelding.Api
{
    public delegate bool CanWeldDelegate(
        object parent,
        object child,
        Type viewWelder);
}