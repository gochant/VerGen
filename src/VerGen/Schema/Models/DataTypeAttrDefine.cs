using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    public class DataTypeAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        private string type;

        [XmlAttribute]
        public string Type
        {
            get { return type; }
            set {
                SetField(ref type, value);
            }
        }

        public override bool Enabled => !string.IsNullOrEmpty(Type);

        public string ToDataAnnotationString()
        {
            var typStr = Type;
            return $"[DataType(DataType.{typStr})]";
        }
    }
}