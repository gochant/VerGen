using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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

        public SelectModelDialog(ObservableCollection<EntitySetListItem> fields, SelectionMode mode = SelectionMode.Multiple) : this()
        {
            if(mode != SelectionMode.Single)
            {
                ChkSelectAll.Visibility = Visibility.Visible;
            }
            else
            {
                ChkSelectAll.Visibility = Visibility.Hidden;
            }
            ListControl.ItemsSource = fields;
            ListControl.SelectionMode = mode;
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
