namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IStateValueInjector
    {
        string Inject(
            IStateContextProvider stateContextProvider,
            string expression);
    }
}