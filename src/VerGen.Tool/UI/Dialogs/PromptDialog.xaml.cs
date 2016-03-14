using System.Windows;

namespace VerGen.Tool.UI.Dialogs
{
    /// <summary>
    /// PromptDialog.xaml 的交互逻辑
    /// </summary>
    public partial class PromptDialog : Window
    {
        public PromptDialog()
        {
            InitializeComponent();
        }

        public PromptDialog(string title):this()
        {
            TxtTitle.Text = title;
        }

        public string ResponseText
        {
            get { return TbResult.Text; }
            set { TbResult.Text = value; }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
