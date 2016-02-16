using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace VerGen.Utility
{
    /// <summary>
    /// Edm元数据加载器
    /// </summary>
    public class EdmMetadataLoader
    {
        public readonly System.Collections.IList Errors = new CompilerErrorCollection();
        public static void ArgumentNotNull<T>(T arg, string name) where T : class
        {
            if (arg == null)
            {
                throw new ArgumentNullException(name);
            }
        }


        public IEnumerable<GlobalItem> CreateEdmItemCollection(string sourcePath)
        {
            ArgumentNotNull(sourcePath, "sourcePath");

            if (!ValidateInputPath(sourcePath))
            {
                return new EdmItemCollection();
            }

            var schemaElement = LoadRootElement(sourcePath);
            if (schemaElement != null)
            {
                using (var reader = schemaElement.CreateReader())
                {
                    IList<EdmSchemaError> errors;
                    var itemCollection = EdmItemCollection.Create(new[] { reader }, null, out errors);

                    ProcessErrors(errors, sourcePath);

                    return itemCollection ?? new EdmItemCollection();
                }
            }
            return new EdmItemCollection();
        }

        public string GetModelNamespace(string sourcePath)
        {
            ArgumentNotNull(sourcePath, "sourcePath");

            if (!ValidateInputPath(sourcePath))
            {
                return string.Empty;
            }

            var model = LoadRootElement(sourcePath);
            if (model == null)
            {
                return string.Empty;
            }

            var attribute = model.Attribute("Namespace");
            return attribute?.Value ?? "";
        }

        private bool ValidateInputPath(string sourcePath)
        {
            if (sourcePath == "$" + "edmxInputFile" + "$")
            {
                Errors.Add(
                    new CompilerError(sourcePath, 0, 0, string.Empty,
                        "Template_ReplaceVsItemTemplateToken"));
                return false;
            }

            return true;
        }

        public XElement LoadRootElement(string sourcePath)
        {
            ArgumentNotNull(sourcePath, "sourcePath");

            var root = XElement.Load(sourcePath, LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
            return root.Elements()
                .Where(e => e.Name.LocalName == "Runtime")
                .Elements()
                .Where(e => e.Name.LocalName == "ConceptualModels")
                .Elements()
                .FirstOrDefault(e => e.Name.LocalName == "Schema")
                   ?? root;
        }

        private void ProcessErrors(IEnumerable<EdmSchemaError> errors, string sourceFilePath)
        {
            foreach (var error in errors)
            {
                Errors.Add(
                    new CompilerError(
                        error.SchemaLocation ?? sourceFilePath,
                        error.Line,
                        error.Column,
                        error.ErrorCode.ToString(CultureInfo.InvariantCulture),
                        error.Message)
                    {
                        IsWarning = error.Severity == EdmSchemaErrorSeverity.Warning
                    });
            }
        }
    }
}