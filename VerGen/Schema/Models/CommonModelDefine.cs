using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    /// <summary>
    /// 公共模型定义
    /// </summary>
    public class CommonModelDefine : ViewModelBase
    {
        #region Properties, Indexers

        [XmlIgnore]
        public EntitySet EntitySet { get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public ObservableCollection<ModelFieldDefine> Fields { get; set; }
            = new ObservableCollection<ModelFieldDefine>();

        #endregion

        public void LoadDynamicData(EntitySet set)
        {
            EntitySet = set;
            foreach (var field in Fields)
            {
                var prop = set.ElementType.Properties.FirstOrDefault(d => d.Name == field.Name);
                field.LoadDynamicData(prop);
            }
        }
    }
}
