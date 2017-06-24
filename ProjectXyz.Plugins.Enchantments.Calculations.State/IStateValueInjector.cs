namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public interface IStateValueInjector
    {
        string Inject(
            IStateContextProvider stateContextProvider,
            string expression);
    }
}