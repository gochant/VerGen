using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;
using VerGen.Schema.Models;
using VerGen.Tool.UI.Dialogs;
using VerGen.Tool.UI.Infrastructure;

namespace VerGen.Tool.UI.ViewModels
{
    public class BusinessModelPackageViewModel: BusinessModelPackage
    {
        public BusinessModelPackageViewModel()
        {
            CommonModel = new CommonModelDefineViewModel();
            CrudUserStory = new CrudUserStoryViewModel();
        }

        #region Fields

        private ViewModelDefineViewModel currentViewModel;

        #endregion

        #region Properties, Indexers



        [XmlIgnore]
        public ViewModelDefineViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set { SetField(ref currentViewModel, value); }
        }

        #region Commands

        public ICommand AddViewModelCommand => new RelayCommand(AddViewModel);
        public ICommand DeleteViewModelCommand => new RelayCommand(DeleteViewModel);

        #endregion

        #endregion

        #region Methods

        public void AddViewModel()
        {
            var promptDlg = new PromptDialog("请输入视图模型名（不包含基础模型名）") { Owner = AppData.MainWindow };
            promptDlg.ShowDialog();
            if (promptDlg.DialogResult == true)
            {
                var name = promptDlg.ResponseText;
                var vm = CreateViewModel(name, new List<string>(), Display);
                if (vm != null)
                {
                    ViewModels.Add(vm);
                    CurrentViewModel = vm;
                }

            }
        }

        public void DeleteViewModel()
        {
            UIHelper.ConfirmOperate(() =>
            {
                ViewModels?.Remove(CurrentViewModel);
                CurrentViewModel = null;
            });
        }

        /// <summary>
        /// 创建视图模型
        /// </summary>
        /// <param name="name">模型名称（不包含实体名称）</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public ViewModelDefineViewModel CreateViewModel(string name, List<string> fields, string display = null)
        {
            var vmName = Name + name;
            if (ViewModels.FirstOrDefault(d => d.Name == vmName) == null)
            {
                var vm = new ViewModelDefineViewModel
                {
                    Name = vmName,
                    DisplayName = display ?? name
                };
                vm.Initialize(fields);
                return vm;
            }
            return null;
        }

        public ViewModelDefineViewModel GetViewModel(string name)
        {
            return (ViewModelDefineViewModel)ViewModels.FirstOrDefault(d => d.Name == name);
        }

        public void Initialize(EntitySet set)
        {
            if (set == Set) return;
            Name = set.ElementType.Name;
            LoadDynamicData(set, false);

            //TODO: 处理列字段变更
            ((CommonModelDefineViewModel)CommonModel).Initialize(set);

            ((CrudUserStoryViewModel)CrudUserStory).Initialize();
        }

        #endregion
    }
}