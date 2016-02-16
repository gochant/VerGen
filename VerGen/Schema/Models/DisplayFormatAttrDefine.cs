using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    public class DisplayFormatAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        [XmlAttribute]
        public string DataFormatString { get; set; }

        public override bool Enabled => !string.IsNullOrEmpty(DataFormatString);
        public string ToDataAnnotationString()
        {
            return $"[DisplayFormat(DataFormatString = \"{DataFormatString}\")]";
        }
    }
}