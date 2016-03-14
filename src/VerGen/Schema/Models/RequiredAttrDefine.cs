using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    /// <summary>
    /// 必填项属性定义
    /// </summary>
    public class RequiredAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        /// <summary>
        /// 允许空字符串
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