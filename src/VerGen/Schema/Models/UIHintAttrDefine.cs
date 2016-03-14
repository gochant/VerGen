using System.Collections.Generic;
using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    public class UIHintAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        [XmlAttribute]
        public string Hint { get; set; }
        public override bool Enabled => !string.IsNullOrEmpty(Hint);

        public string ToDataAnnotationString()
        {
            return $"[UIHint(\"{Hint}\")]";
        }

        public static List<string> GetDefaultUIHint()
        {
            return new List<string>
            {
                "CheckboxList",
                "ComboBox",
                "Hidden",
                "InputButton",
                "MultiSelect",
                "Select",
                "StaticText",
                "Year"
            };
        }
    }
}