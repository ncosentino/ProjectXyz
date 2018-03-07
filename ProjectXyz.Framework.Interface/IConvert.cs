namespace ProjectXyz.Framework.Interface
{
    public interface IConvert<in T1, out T2>
    {
        T2 Convert(T1 input);
    }
}
