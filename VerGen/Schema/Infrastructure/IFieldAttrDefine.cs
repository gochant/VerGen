namespace VerGen.Schema.Infrastructure
{
    public interface IFieldAttrDefine
    {
        bool IsApplicable { get; set; }
        bool Enabled { get; set; }
    }
}