using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace VerGen.Schema.Models
{
    /// <summary>
    /// 视图模型定义
    /// </summary>
    public class ViewModelDefine
    {
        /// <summary>
        /// 名称
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [XmlAttribute]
        public string DisplayName { get; set; }

        /// <summary>
        /// 启用逆向映射
        /// </summary>
        [XmlAttribute]
        public bool EnableReverseMap { get; set; } = false;

        /// <summary>
        /// 字段
        /// </summary>
        public ObservableCollection<ViewModelFieldDefine> Fields { get; set; }
            = new ObservableCollection<ViewModelFieldDefine>();

        public void Initialize(List<string> fields)
        {
            foreach (var fieldName in fields)
            {
                AddField(fieldName);
            }
        }

        public void AddField(string fieldName)
        {
            var exist = Fields.FirstOrDefault(d => d.Name == fieldName);
            if (exist == null)
            {
                var field = new ViewModelFieldDefine();
                field.Initialize(fieldName);
                Fields.Add(field);
            }
            else
            {
                exist.Initialize(fieldName);
            }
        }

        public void AddFields(IEnumerable<string> fields, bool removeInvalid = false)
        {
            var fieldList = fields.ToList();
            foreach (var field in fieldList)
            {
                AddField(field);
            }

            // 移除无效的字段
            if (removeInvalid)
            {

                foreach (var field in Fields.Where(d => !fieldList.Contains(d.Name)).ToList())
                {
                    Fields.Remove(field);
                }

            }
        }
    }
}
