using EnvDTE;
using VerGen.Tool.Infrastructure;
using VerGen.Tool.UI;

namespace VerGen.Tool.Commands
{
    public class ManageBusinessModelCommand : CommandHandler
    {
        public ManageBusinessModelCommand() : base()
        {
            Kind = CommandEffectiveKind.File;
            CommandId = PkgCmdIdList.ManageBusinessModel;
        }


        public override void ItemInvokeHandler(Project project, SelectedItem item)
        {
            var wnd = new MainWindow(project, item);
            wnd.ShowDialog();
        }
    }
}