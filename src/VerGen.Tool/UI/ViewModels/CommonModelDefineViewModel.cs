using System.Collections.Generic;
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

        [XmlIgnore]
        public ICommand SyncCommand => new RelayCommand(Sync);

        [XmlIgnore]
        public ICommand FixedFieldCommand => new RelayCommand(FixedField);

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

        public void Sync()
        {
            Initialize();
        }

        public void FixedField()
        {
            var props = GetSimpleProperties().ToList();
            var selectList = props.Select(d => new SelectListItem
            {
                Text = d.Documentation?.Summary,
                Value = d.Name
            });

            var dlg = new SelectDbFieldDialog(selectList) { Owner = AppData.MainWindow };
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                var fieldName = dlg.Selected;
                CurrentField.Initialize(props.FirstOrDefault(d => d.Name == fieldName));
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
            if (field?.IsCalculated == false && field?.Invalid == false)
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

        public void Initialize(EntitySet set = null)
        {
            if (set != null)
            {
                EntitySet = set;
            }

            var hasHandleds = new List<string>();
            // TODO: 这里暂时没有处理复杂类型
            foreach (var prop in GetSimpleProperties())
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

                hasHandleds.Add(field.Name);
            }

            // 验证有效性
            var invalidFields = Fields.Where(
                d =>
                    !hasHandleds.Contains(d.Name) &&
                    (!d.IsCalculated || d.IsCalculated && !hasHandleds.Contains(d.AssociatedField)));

            foreach (var field in invalidFields)
            {
                field.Invalid = true;
            }
        }

        /// <summary>
        /// 清理无效字段
        /// </summary>
        public void ClearInvalidFields()
        {

        }

        private IEnumerable<EdmProperty> GetSimpleProperties()
        {
            return EntitySet.ElementType.Properties
                .Where(d => !d.IsComplexType);
        }

        #endregion
    }
}
