namespace ProjectXyz.Api.Framework
{
    public interface IIdentifierConverter
    {
        bool TryConvert(
            object input,
            out IIdentifier output);

        bool TryConvert<T>(
            T input,
            out IIdentifier output);

        IIdentifier Convert(object input);

        IIdentifier Convert<T>(T input);
    }
}