using ProjectXyz.Plugins.Api;

namespace ProjectXyz.Api.States.Plugins
{
    public interface IStatePlugin : IPlugin
    {
        IStateIdToTermRepository StateIdToTermRepository { get; }

        IStateContextProvider StateContextProvider { get; }
    }
}