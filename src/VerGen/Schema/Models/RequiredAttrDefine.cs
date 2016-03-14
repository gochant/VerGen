using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    /// <summary>
    /// ���������Զ���
    /// </summary>
    public class RequiredAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        /// <summary>
        /// ������ַ���
        /// </summary>
        [XmlAttribute]
        public bool AllowEmptyStrings { get; set; } = false;

        public string ToDataAnnotationString()
        {
            return AllowEmptyStrings ? $"[Required(AllowEmptyStrings = {AllowEmptyStrings.ToString().ToLower()})]" 
                : "[Required]";
        }
    }
}