using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    /// <summary>
    /// 业务模型包
    /// </summary>
    public class BusinessModelPackage: ViewModelBase
    {
        public BusinessModelPackage()
        {
            commonModel = new CommonModelDefine();
            crudUserStory = new CrudUserStory();
        }

        #region Fields

        private CommonModelDefine commonModel;
        private string name;
        private CrudUserStory crudUserStory;
        private EntitySet set;
        private string display;

        #endregion

        #region Properties, Indexers

        [XmlIgnore]
        public EntitySet Set
        {
            get { return set; }
            set { SetField(ref set, value); }
        }

        /// <summary>
        /// 模型名
        /// </summary>
        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { SetField(ref name, value); }
        }

        /// <summary>
        /// 显示名
        /// </summary>
        [XmlAttribute]
        public string Display
        {
            get { return display; }
            set { SetField(ref display, value); }
        }


        /// <summary>
        /// 公共模型定义
        /// </summary>
        public CommonModelDefine CommonModel
        {
            get { return commonModel; }
            set { SetField(ref commonModel, value); }
        }

        /// <summary>
        /// 视图模型列表
        /// </summary>
        public ObservableCollection<ViewModelDefine> ViewModels { get; set; } 
            = new ObservableCollection<ViewModelDefine>();

        /// <summary>
        /// CRUD用户场景
        /// </summary>
        public CrudUserStory CrudUserStory
        {
            get { return crudUserStory; }
            set { SetField(ref crudUserStory, value); }
        }

        #endregion

        public void LoadDynamicData(EntitySet set, bool callInternal = true)
        {
            if (set == Set) return;

            Set = set;
            Display = set.ElementType.Documentation?.Summary ?? set.Name;

            if (callInternal)
            {
                CommonModel.LoadDynamicData(Set);
            }
        }

    }
}