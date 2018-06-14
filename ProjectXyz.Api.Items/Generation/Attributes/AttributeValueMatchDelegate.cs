namespace ProjectXyz.Api.Items.Generation.Attributes
{
    public delegate bool AttributeValueMatchDelegate(
        IItemGeneratorAttributeValue v1,
        IItemGeneratorAttributeValue v2);
}