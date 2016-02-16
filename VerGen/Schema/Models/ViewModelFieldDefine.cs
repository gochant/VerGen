using System.Xml.Serialization;

namespace VerGen.Schema.Models
{
    /// <summary>
    /// 视图模型字段定义
    /// </summary>
    public class ViewModelFieldDefine
    {
        /// <summary>
        /// 名称
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// 是骨架代码
        /// </summary>
        [XmlAttribute]
        public bool IsScaffolding { get; set; }

        public void Initialize(string name)
        {
            Name = name;
        }
    }
}
