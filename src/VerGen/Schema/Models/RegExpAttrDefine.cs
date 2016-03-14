using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    public class RegExpAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        [XmlAttribute]
        public string Pattern { get; set; }

        [XmlAttribute]
        public string ErrorMsg { get; set; }

        public string ToDataAnnotationString()
        {
            return $"[RegularExpression(@\"{Pattern}\", ErrorMessage = \"{{0}}{ErrorMsg}\")]";
        }
    }
}