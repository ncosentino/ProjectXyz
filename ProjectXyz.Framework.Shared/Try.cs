using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Framework.Shared
{
    public static class Try
    {
        private static readonly ITry IN_RELEASE_MODE = new TryInReleaseMode();

        public static ITry InReleaseMode => IN_RELEASE_MODE;
    }
}