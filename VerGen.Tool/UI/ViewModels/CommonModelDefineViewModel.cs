using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using VerGen.Schema.Models;
using VerGen.Tool.UI.Dialogs;
using VerGen.Tool.UI.Infrastructure;

namespace VerGen.Tool.UI.ViewModels
{
    public class CommonModelDefineViewModel : CommonModelDefine
    {
        #region Fields

        private ModelFieldDefineViewModel currentField;

        #endregion

        #region Properties, Indexers

        [XmlIgnore]
        public ModelFieldDefineViewModel CurrentField
        {
            get { return currentField; }
            set { SetField(ref currentField, value); }
        }

        #region Commands

        [XmlIgnore]
        public ICommand DeleteFieldCommand => new RelayCommand(DeleteCurrentField);

        [XmlIgnore]
        public ICommand AddFieldCommand => new RelayCommand(AddField);

        #endregion

        #endregion

        #region Methods

        public void AddField(EdmProperty edmProp)
        {
            var field = new ModelFieldDefineViewModel()
            {
                Name = edmProp.Name,
                EdmProperty = edmProp
            };
            Fields.Add(field);
        }

        public void AddField()
        {
            var promptDlg = new PromptDialog("请输入字段名称") { Owner = AppData.MainWindow };
            promptDlg.ShowDialog();
            if (promptDlg.DialogResult == true)
            {
                var fieldName = promptDlg.ResponseText;
                var field = AddField(fieldName);
                CurrentField = field;
            }
        }

        public ModelFieldDefineViewModel AddField(string fieldName)
        {
            var field = CreateField(fieldName);
            Fields.Add(field);
            return field;
        }

        public ModelFieldDefineViewModel CreateField(string fieldName)
        {
            var field = new ModelFieldDefineViewModel { IsCalculated = true };
            field.Initialize(fieldName);
            return field;
        }

        public void DeleteCurrentField()
        {
            DeleteField();
        }

        public void DeleteField(ModelFieldDefineViewModel field = null)
        {
            field = field ?? CurrentField;
            if (field?.IsCalculated == false)
            {
                MessageBox.Show("非计算字段不能删除", string.Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UIHelper.ConfirmOperate(() =>
             {
                 Fields?.Remove(field);
                 CurrentField = null;
             });
        }

        public ModelFieldDefineViewModel GetField(string name)
        {
            return (ModelFieldDefineViewModel)Fields.FirstOrDefault(d => d.Name == name);
        }

        //public void LoadDynamicData(EntitySet set)
        //{
        //    foreach (var field in Fields)
        //    {
        //        var prop = set.ElementType.Properties.FirstOrDefault(d => d.Name == field.Name);
        //        ((ModelFieldDefineViewModel)field).LoadDynamicData(prop);
        //    }
        //}

        public void Initialize(EntitySet set)
        {
            // TODO: 这里暂时没有处理复杂类型
            foreach (var prop in set.ElementType.Properties
                .Where(d => !d.IsComplexType))
            {
                var field = (ModelFieldDefineViewModel)Fields.FirstOrDefault(d => d.Name == prop.Name);

                // 更新现有字段
                if (field != null)
                {
                    field.Initialize(prop);
                }
                // 添加不存在的字段
                else
                {
                    field = new ModelFieldDefineViewModel();
                    field.Initialize(prop);
                    Fields.Add(field);
                }

            }

        }

        /// <summary>
        /// 清理无效字段
        /// </summary>
        public void ClearInvalidFields()
        {

        }

        #endregion
    }
}
