using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using VerGen.Tool.Utilities;

namespace VerGen.Tool.Infrastructure
{
    public enum CommandEffectiveKind
    {
        Project,
        DbContext,
        File
    }

    public class CommandHandler
    {
        public static readonly Guid CommandSet = new Guid("48187f2b-dcad-419c-a226-bebdf737dd1c");

        public int CommandId { get; set; }
        public ThisPackage Package { get; set; }

        public CommandEffectiveKind Kind { get; set; }

        private IServiceProvider ServiceProvider => this.Package;

        public virtual void Initialize(ThisPackage package)
        {
            if (package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }

            Package = package;

            var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandId = new CommandID(CommandSet, CommandId);
                var menuItem = new OleMenuCommand(InvokeHandler, null,
                    BeforeQueryStatus, menuCommandId);
                //new MenuCommand(this.MenuItemCallback, menuCommandId);
                commandService.AddCommand(menuItem);
            }
        }


        public CommandHandler()
        {

        }

        private void BeforeQueryStatus(object sender, EventArgs e)
        {
            if (Kind == CommandEffectiveKind.File)
            {
                OnItemMenuBeforeQueryStatus(sender, e);
            }
            if (Kind == CommandEffectiveKind.Project)
            {
                OnProjectMenuBeforeQueryStatus(sender, e);
            }
        }


        private void OnProjectMenuBeforeQueryStatus(object sender, EventArgs e)
        {
            var menuCommand = sender as MenuCommand;

            if (menuCommand == null)
            {
                return;
            }

            if (Package.Dte2.SelectedItems.Count != 1)
            {
                return;
            }

            var project = Package.Dte2.SelectedItems.Item(1).Project;

            if (project == null)
            {
                return;
            }

            menuCommand.Visible =
                project.Kind == "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"; // csproj
        }

        private void InvokeHandler(object sender, EventArgs e)
        {
            var menuCommand = sender as MenuCommand;
            if (menuCommand == null || Package.Dte2.SelectedItems.Count != 1)
            {
                return;
            }

            var selectedItem = Package.Dte2.SelectedItems.Item(1);
            var project = selectedItem.Project ?? selectedItem.ProjectItem.ContainingProject;
            if (project == null)
            {
                return;
            }

            if (Kind == CommandEffectiveKind.Project)
            {
                ProjectInvokeHandler(project);
            }
            if (Kind == CommandEffectiveKind.File)
            {
                ItemInvokeHandler(project, selectedItem);
            }

        }

        public virtual void ContextInvokeHandler(object context, Type systemContextType)
        {
            throw new NotImplementedException();
        }

        private void OnItemMenuBeforeQueryStatus(object sender, EventArgs e)
        {
            //OnItemMenuBeforeQueryStatus(
            //    sender,
            //    new[] { FileExtensions.CSharp, FileExtensions.VisualBasic, FileExtensions.EntityDataModel });
            OnItemMenuBeforeQueryStatus(
                sender,
                new[] { FileExtensions.BusinessModel });
        }

        private void OnItemMenuBeforeQueryStatus(object sender, IEnumerable<string> supportedExtensions)
        {
            var menuCommand = sender as MenuCommand;

            if (menuCommand == null)
            {
                return;
            }

            if (Package.Dte2.SelectedItems.Count != 1)
            {
                return;
            }

            var extensionValue = GetSelectedItemExtension();
            menuCommand.Visible = supportedExtensions.Contains(extensionValue);
        }


        public string GetSelectedItemExtension()
        {
            var selectedItem = Package.Dte2.SelectedItems.Item(1);

            var extension = selectedItem.ProjectItem?.Properties?.Item("Extension");

            return (string)extension?.Value;
        }


        private static IEnumerable<CodeElement> FindClassesInCodeModel(CodeElements codeElements)
        {
            Contract.Requires(codeElements != null);

            foreach (CodeElement codeElement in codeElements)
            {
                if (codeElement.Kind == vsCMElement.vsCMElementClass)
                {
                    yield return codeElement;
                }

                foreach (var element in FindClassesInCodeModel(codeElement.Children))
                {
                    yield return element;
                }
            }
        }

        private static void DisableDatabaseInitializer(Type userContextType, Type systemContextType)
        {
            Contract.Requires(userContextType != null);
            Contract.Requires(systemContextType != null);

            var databaseType = systemContextType.Assembly.GetType("System.Data.Entity.Database");

            var setInitializerMethodInfo
                = databaseType?.GetMethod("SetInitializerInternal", BindingFlags.NonPublic | BindingFlags.Static);

            if (setInitializerMethodInfo != null)
            {
                var boundSetInitializerMethodInfo
                    = setInitializerMethodInfo.MakeGenericMethod(userContextType);

                boundSetInitializerMethodInfo.Invoke(null, new object[] { null, true });
            }
        }





        public virtual void ProjectInvokeHandler(Project project)
        {
        }
        
        public virtual void ItemInvokeHandler(Project project, SelectedItem item)
        {

        }
    }
}