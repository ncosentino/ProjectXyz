namespace ProjectXyz.Application.Interface.Enchantments.Calculations
{
    public interface IStateValueInjector
    {
        string Inject(
            IStateContextProvider stateContextProvider,
            string expression);
    }
}