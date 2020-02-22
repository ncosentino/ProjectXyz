using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation
{
    public sealed class TargetNavigator : ITargetNavigator
    {
        // self
        // owner.self
        // owner
        // owner.owner.self
        public IIdentifier NavigateUp(IIdentifier identifier)
        {
            var id = identifier?.ToString() ?? string.Empty;
            if (id.StartsWith("owner."))
            {
                id = id.Substring("owner.".Length);
            }
            else if (id.StartsWith("owner"))
            {
                id = id.Substring("owner".Length);
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                id = "self";
            }

            return new StringIdentifier(id);
        }

        public IIdentifier NavigateDown(IIdentifier identifier)
        {
            var id = identifier?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(id))
            {
                id = "self";
            }

            id = $"owner.{id}";
            return new StringIdentifier(id);
        }

        public bool IsSelf(IIdentifier identifier)
        {
            var id = identifier?.ToString() ?? string.Empty;
            return
                string.IsNullOrWhiteSpace(id) ||
                id == "self";
        }

        public bool AreTargetsEqual(IIdentifier target1, IIdentifier target2)
        {
            if (target1 == null && target2 == null)
            {
                return true;
            }

            if (target1 == null && target2 != null ||
                target1 != null && target2 == null)
            {
                return false;
            }

            if (target1.Equals(target2) ||
                IsSelf(target1) && IsSelf(target2))
            {
                return true;
            }

            return target1.ToString().Replace(".self", string.Empty).Equals(
                target2.ToString().Replace(".self", string.Empty));
        }
    }
}
