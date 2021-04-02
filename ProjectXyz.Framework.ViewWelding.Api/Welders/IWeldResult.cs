namespace ProjectXyz.Framework.ViewWelding.Api.Welders
{
    public interface IWeldResult
    {
        object Parent { get; }

        object Child { get; }
    }
}