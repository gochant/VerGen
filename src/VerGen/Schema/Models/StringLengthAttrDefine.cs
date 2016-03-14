using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    public class StringLengthAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        [XmlAttribute]
        public int Max { get; set; } = 50;

        [XmlAttribute]
        public int Min { get; set; }
        public string ToDataAnnotationString()
        {
            return $"[StringLength({Max}, MinimumLength = {Min})]";
        }
    }
}