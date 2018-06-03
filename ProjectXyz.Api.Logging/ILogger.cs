namespace ProjectXyz.Api.Logging
{
    public interface ILogger
    {
        void Debug(string message);

        void Debug(string message, object data);

        void Info(string message);

        void Info(string message, object data);

        void Warn(string message);

        void Warn(string message, object data);

        void Error(string message);

        void Error(string message, object data);
    }
}
