namespace VerGen.Schema.Infrastructure
{
    public interface IDataAnnontationAttrDefine : IFieldAttrDefine
    {
        string ToDataAnnotationString();
    }
}