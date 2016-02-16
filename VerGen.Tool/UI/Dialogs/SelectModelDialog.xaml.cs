using System.Collections.ObjectModel;
using System.Windows;
using VerGen.Tool.UI.ViewModels;

namespace VerGen.Tool.UI.Dialogs
{
    /// <summary>
    /// SelectModelDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SelectModelDialog
    {
        public SelectModelDialog()
        {
            InitializeComponent();
            ListControl = LvConceptModelSelect;
            SelectAllControl = ChkSelectAll;
        }

        public SelectModelDialog(ObservableCollection<EntitySetListItem> fields) : this()
        {
            ListControl.ItemsSource = fields;
        }


        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            SetResult<EntitySetListItem>(d => d.ModelName);
        }

        private void ChkSelectAll_Click(object sender, RoutedEventArgs e)
        {
            SelectAll();
        }
    }
}
