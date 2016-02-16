using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VerGen.Tool.UI.ViewModels;

namespace VerGen.Tool.UI.Dialogs
{
    /// <summary>
    /// SelectFieldDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SelectFieldsDialog : Window
    {

        public SelectFieldsDialog()
        {
            InitializeComponent();
        }

        public IEnumerable<string> Selected { get; set; }

        public SelectFieldsDialog(IEnumerable<ModelFieldDefineViewModel> fields) : this()
        {
            LvFieldSelect.ItemsSource = fields;
        }


        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Selected = LvFieldSelect.SelectedItems.Cast<ModelFieldDefineViewModel>().Select(d => d.Name).ToList();
            DialogResult = true;
        }

        private void ChkSelectAll_Click(object sender, RoutedEventArgs e)
        {
            if (ChkSelectAll.IsChecked == true)
            {
                LvFieldSelect.SelectAll();
                LvFieldSelect.Focus();
            }
            else
            {
                LvFieldSelect.UnselectAll();
            }
        }
    }
}
