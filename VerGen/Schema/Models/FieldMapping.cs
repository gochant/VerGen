using System.Xml.Serialization;

namespace VerGen.Schema.Models
{
    public class FieldMap
    {
        /// <summary>
        /// 作为表达式
        /// </summary>
        [XmlAttribute]
        public bool IsUsedAsExpression { get; set; } = true;

        /// <summary>
        /// 代码
        /// </summary>
        [XmlAttribute]
        public string SourceCode { get; set; }
    }
}
