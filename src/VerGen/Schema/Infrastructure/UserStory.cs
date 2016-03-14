using System.Xml.Serialization;

namespace VerGen.Schema.Infrastructure
{
    public class UserStory
    {
        /// <summary>
        /// 用户场景名称
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
    }
}
