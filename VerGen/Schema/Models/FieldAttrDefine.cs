using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    public class FieldAttrDefine : ViewModelBase, IFieldAttrDefine
    {
        /// <summary>
        /// 是否适用
        /// </summary>
        [XmlAttribute]
        public virtual bool IsApplicable { get; set; } = true;

        /// <summary>
        /// 是否启用
        /// </summary>
        [XmlAttribute]
        public virtual bool Enabled { get; set; }

    }
}
