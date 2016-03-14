using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    public class DisplayFieldAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        private string fieldName;

        [XmlAttribute]
        public string FieldName
        {
            get { return fieldName; }
            set {
                SetField(ref fieldName, value);
            }
        }

        public override bool Enabled => !string.IsNullOrEmpty(FieldName);

        public string ToDataAnnotationString()
        {
            return $"[DisplayField(\"{FieldName}\")]";
        }
    }
}