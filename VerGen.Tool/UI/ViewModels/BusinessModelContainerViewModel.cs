using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using VerGen.Schema.Models;
using VerGen.Tool.EfUtilities;
using VerGen.Tool.UI.Dialogs;
using VerGen.Tool.UI.Infrastructure;
using VerGen.Tool.Utilities;

namespace VerGen.Tool.UI.ViewModels
{
    public class BusinessModelContainerViewModel : BusinessModelContainer
    {

        #region Fields

        private BusinessModelPackage currentPackage;

        #endregion

        #region Properties, Indexers

        //[XmlIgnore]
        //public EdmItemCollection Edm { get; set; }

        [XmlIgnore]
        public ObservableCollection<SelectListItem> DataTypeList => DataTypeHelper.GetDataTypeList();

        [XmlIgnore]
        public List<string> UIHintList => UIHintAttrDefine.GetDefaultUIHint();

        /// <summary>
        /// 未使用的实体集 列表
        /// </summary>
        [XmlIgnore]
        public ObservableCollection<EntitySetListItem> UnusedEntitySetList
        {
            get
            {
                var defines = GetPackageSelectList();
                var result = GetEntitySets()
                    .Where(d => defines.All(f => f.Name != d.ElementType.Name)).Select(d => new EntitySetListItem
                    {
                        Name = d.Name,
                        ModelName = d.ElementType.Name,
                        Display = d.ElementType.Documentation?.Summary
                    }).ToList();
                return new ObservableCollection<EntitySetListItem>(result);
            }
        }

        [XmlIgnore]
        public BusinessModelPackage CurrentPackage
        {
            get { return currentPackage; }
            set { SetField(ref currentPackage, value); }
        }

        #region Commands

        public ICommand AddPackageCommand => new RelayCommand(AddPackage);
        public ICommand DeletePackageCommand => new RelayCommand(DeletePackage);
        public ICommand SaveCommand => new RelayCommand(SaveToFile);

        #endregion

        #endregion

        #region Methods

        public void LoadEdm(string edmPath = null)
        {
            var itemCollection = new EdmMetadataLoader().CreateEdmItemCollection(edmPath);
            LoadEdm((EdmItemCollection)itemCollection);
        }

        public List<PackageListItem> GetPackageSelectList()
        {
            var sets = GetEntitySets();
            return Packages.Select(d => new PackageListItem
            {
                Name = d.Name,
                IsValid = sets.Any(e => e.Name == d.Name)
            }).ToList();
        }

        public void AddPackage()
        {
            var dlg = new SelectModelDialog(UnusedEntitySetList) { Owner = AppData.MainWindow };
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                var names = dlg.Selected;
                foreach (var name in names)
                {
                    CreatePackage(name);
                }
            }
        }

        public void DeletePackage()
        {
            UIHelper.ConfirmOperate(() =>
            {
                Packages?.Remove(CurrentPackage);
                CurrentPackage = null;
            });
        }

        public BusinessModelPackageViewModel CreatePackage(string name)
        {
            var set = GetEntitySet(name);
            if (set != null)
            {
                var package = new BusinessModelPackageViewModel();
                package.Initialize(set);
                Packages.Add(package);
                return package;
            }

            return null;
        }

        public BusinessModelPackage GetPackage(string name)
        {
            return Packages.FirstOrDefault(d => d.Name == name);
        }

        public void SaveToFile()
        {
            var serializer = CreateSerializer();
            
            using (var stream = File.Create(SaveFilePath))
            {
                serializer.Serialize(stream, this);
            }
            MessageBox.Show("保存成功", "结果", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static XmlSerializer CreateSerializer()
        {
            var serializer = new XmlSerializer(typeof(BusinessModelContainer),
                     new[] {
                         typeof(BusinessModelContainerViewModel),
                    typeof(BusinessModelPackageViewModel),
                    typeof(CommonModelDefineViewModel),
                    typeof(CrudUserStoryViewModel),
                    typeof(ModelFieldDefineViewModel),
                    typeof(ViewModelDefineViewModel),
                     });
            return serializer;
        }

        #endregion
    }
}