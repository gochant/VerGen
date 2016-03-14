using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using Window = System.Windows.Window;

namespace VerGen.Tool.UI
{
    public static class AppData
    {
        public static List<Window> WindowStack { get; set; }
            = new List<Window>();

        public static MainWindow MainWindow { get; set; } 

        public static Window GetMainWindow()
        {
            return WindowStack.Last();
        }

        public static Project CurrentProject => MainWindow?.Project;
    }
}