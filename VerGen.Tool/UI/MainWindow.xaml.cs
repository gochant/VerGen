using System;
using System.IO;
using System.Windows.Input;
using EnvDTE;
using VerGen.Schema.Models;
using VerGen.Tool.Extensions;
using VerGen.Tool.UI.ViewModels;

namespace VerGen.Tool.UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public Project Project { get; set; }
        public MainWindow(Project project, SelectedItem item)
        {
            InitializeComponent();
            AppData.MainWindow = this;
            Project = project;

            var bmFilePath = item.ProjectItem.Properties.Item("FullPath").Value.ToString();

            var container = Load(bmFilePath);
            
            DataContext = container;

        }
        public BusinessModelContainerViewModel Load(string bmFilePath)
        {
            var container = BusinessModelContainer.LoadFromFile<BusinessModelContainerViewModel>(bmFilePath, BusinessModelContainerViewModel.CreateSerializer());
            //var container = BusinessModelContainer.LoadFromFile<BusinessModelContainerViewModel>(bmFilePath);
            if (container == null) return null;

            container.SaveFilePath = bmFilePath;

            var projDir = Project.GetProjectDir();
            var relativePath = container.EdmFilePath ?? @"Designer\ER.edmx";
            var edmFilePath = Path.Combine(projDir, relativePath);

            container.EdmFilePath = relativePath;
            container.LoadEdm(edmFilePath);
            container.Sync();
            return container;
        }

        private void Numeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;

            }
        }

        protected override void OnClosed(EventArgs e)
        {
            AppData.MainWindow = null;
            base.OnClosed(e);
        }
    }
}
