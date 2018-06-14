namespace ProjectXyz.Api.Items.Generation.Attributes
{
    public interface IAttributeValueMatcher
    {
        bool Match(
            IItemGeneratorAttributeValue v1,
            IItemGeneratorAttributeValue v2);
    }
}