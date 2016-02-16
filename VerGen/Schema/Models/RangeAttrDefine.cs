using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    public class RangeAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string Max { get; set; }

        [XmlAttribute]
        public string Min { get; set; }

        public string ToDataAnnotationString()
        {
            return $"[Range(typeof({Type}), \"{Min}\", \"{Max}\")]";
        }
    }
}