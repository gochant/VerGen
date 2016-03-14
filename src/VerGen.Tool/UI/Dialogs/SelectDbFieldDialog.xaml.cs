using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VerGen.Tool.UI.Infrastructure;
using VerGen.Tool.UI.ViewModels;

namespace VerGen.Tool.UI.Dialogs
{
    /// <summary>
    /// SelectFieldDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SelectDbFieldDialog : Window
    {

        public SelectDbFieldDialog()
        {
            InitializeComponent();
        }

        public string Selected { get; set; }

        public SelectDbFieldDialog(IEnumerable<SelectListItem> fields) : this()
        {
            LvFieldSelect.ItemsSource = fields;
        }


        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Selected =
                LvFieldSelect.SelectedItems.Cast<SelectListItem>().Select(d => d.Value).FirstOrDefault();
            DialogResult = true;
        }

    }
}
