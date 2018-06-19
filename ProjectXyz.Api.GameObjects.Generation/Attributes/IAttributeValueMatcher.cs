namespace ProjectXyz.Api.GameObjects.Generation.Attributes
{
    public interface IAttributeValueMatcher
    {
        bool Match(
            IGeneratorAttributeValue v1,
            IGeneratorAttributeValue v2);
    }
}