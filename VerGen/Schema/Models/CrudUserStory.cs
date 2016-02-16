using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    /// <summary>
    /// CRUD用户场景
    /// </summary>
    public class CrudUserStory : UserStory
    {
        /// <summary>
        /// 列表模型
        /// </summary>
        [XmlAttribute]
        public string ListItemViewModel { get; set; }

        /// <summary>
        /// 编辑模型
        /// </summary>
        [XmlAttribute]
        public string EditViewModel { get; set; }

    }
}
