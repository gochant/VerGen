using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    public class DisplayAttrDefine : FieldAttrDefine, IDataAnnontationAttrDefine
    {
        /// <summary>
        /// ����
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// �Ƿ����ù��ʻ�
        /// </summary>
        [XmlAttribute]
        public bool IsI18N { get; set; }

        public override bool Enabled => !string.IsNullOrEmpty(Name);

        public string ToDataAnnotationString()
        {
            var resourceType = IsI18N ? "typeof(Resources)" : "null";
            return $"[Display(ResourceType = {resourceType}, Name = \"{Name}\")]";
        }
    }
}