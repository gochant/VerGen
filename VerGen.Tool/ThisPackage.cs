using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using VerGen.Tool.Commands;
using VerGen.Tool.Infrastructure;

namespace VerGen.Tool
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(ThisPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class ThisPackage : Package
    {
        /// <summary>
        /// Command1Package GUID string.
        /// </summary>
        public const string PackageGuidString = "11d43966-4828-4bfb-bb2a-c1fd046d8fb2";

        public DTE2 Dte2 { get; set; }


        public ThisPackage()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
        }

        private Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            path = Path.GetDirectoryName(path);

            if (args.Name.ToLower().Contains("fontawesome.wpf"))
            {
                path = Path.Combine(path, "FontAwesome.WPF.dll");
                Assembly ret = Assembly.LoadFrom(path);
                return ret;
            }
            return null;
        }

        #region Package Members

        protected override void Initialize()
        {
            base.Initialize();

            Dte2 = GetService(typeof(DTE)) as DTE2;

            if (Dte2 == null)
            {
                return;
            }

            AddCommands();
        }

        public void AddCommand<T>() where T : CommandHandler, new()
        {
            var command = new T();
            command.Initialize(this);
        }

        private void AddCommands()
        {
            AddCommand<ManageBusinessModelCommand>();
        }

        internal T GetService<T>() where T : class
        {
            return (T)GetService(typeof(T));
        }

        /// <summary>
        /// 获取ObjectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static dynamic GetObjectContext(dynamic context)
        {
            var objectContextAdapterType
                = context.GetType().GetInterface("System.Data.Entity.Infrastructure.IObjectContextAdapter");

            return objectContextAdapterType.InvokeMember("ObjectContext", BindingFlags.GetProperty, null, context, null);
        }


        /// <summary>
        /// 记录错误
        /// </summary>
        /// <param name="statusMessage"></param>
        /// <param name="exception"></param>
        internal void LogError(string statusMessage, Exception exception)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(statusMessage));
            Contract.Requires(exception != null);

            var edmSchemaErrorException = exception as EdmSchemaErrorException;
            var compilerErrorException = exception as CompilerErrorException;

            Dte2.StatusBar.Text = statusMessage;

            var buildOutputWindow = Dte2.ToolWindows.OutputWindow.OutputWindowPanes.Item("Build");

            buildOutputWindow.OutputString(Environment.NewLine);

            if (edmSchemaErrorException != null)
            {
                buildOutputWindow.OutputString(edmSchemaErrorException.Message + Environment.NewLine);

                foreach (var error in edmSchemaErrorException.Errors)
                {
                    buildOutputWindow.OutputString(error + Environment.NewLine);
                }
            }
            else if (compilerErrorException != null)
            {
                buildOutputWindow.OutputString(compilerErrorException.Message + Environment.NewLine);

                foreach (var error in compilerErrorException.Errors)
                {
                    buildOutputWindow.OutputString(error + Environment.NewLine);
                }
            }
            else
            {
                buildOutputWindow.OutputString(exception + Environment.NewLine);
            }

            buildOutputWindow.Activate();
        }

        public Project GetProject()
        {
            return Dte2.SelectedItems.Item(1).Project;
        }

        #endregion
    }
}
