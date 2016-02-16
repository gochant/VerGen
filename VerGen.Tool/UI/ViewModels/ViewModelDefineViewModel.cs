using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VerGen.Schema.Models;
using VerGen.Tool.UI.Dialogs;
using VerGen.Tool.UI.Infrastructure;

namespace VerGen.Tool.UI.ViewModels
{
    /// <summary>
    /// 视图模型定义
    /// </summary>
    public class ViewModelDefineViewModel: ViewModelDefine
    {

        public ICommand ConfigFieldsCommand => new RelayCommand<IEnumerable<ModelFieldDefine>>(ConfigFields);

        public void ConfigFields(IEnumerable<ModelFieldDefine> fileds)
        {
            var dlg = new SelectFieldsDialog(fileds.Select(d => (ModelFieldDefineViewModel)d)) { Owner = AppData.MainWindow };
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                var fields = dlg.Selected;
                AddFields(fields, true);
            }
        }
    }
}
