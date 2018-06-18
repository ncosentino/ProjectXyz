using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Framework
{
    public static class Try
    {
        private static readonly ITry IN_RELEASE_MODE = new TryInReleaseMode();
        private static readonly ITry FAIL_ON_ERROR = new TryAndFailOnError();

        public static ITry InReleaseMode => IN_RELEASE_MODE;

        public static ITry FailOnError => FAIL_ON_ERROR;
    }
}