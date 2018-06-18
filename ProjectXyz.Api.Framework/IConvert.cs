namespace ProjectXyz.Api.Framework
{
    public interface IConvert<in T1, out T2>
    {
        T2 Convert(T1 input);
    }
}
