using System;
using VerGen.Enums;
using VerGen.Ext;
using VerGen.Schema.Models;

namespace VerGen.TemplateParameters
{
    [Serializable]
    public class ControllerParameter : IControllerParameter, IEntityParameter, IServiceParameter, ITemplateParameter
    {
        #region IControllerParameter Members

        public string DtoNamespace { get; set; }
        public string ControllerNamespace { get; set; }
        public string EntitySetName { get; set; }
        public string EditDtoName { get; set; }
        public string ItemDtoName { get; set; }

        #endregion

        #region IEntityParameter Members

        public string EntityNamespace { get; set; }
        public string EntityName { get; set; }
        public string EntityKeyType { get; set; }

        #endregion

        #region IServiceParameter Members

        public string ServiceNamespace { get; set; }
        public string ServiceName { get; set; }

        #endregion

        #region ITemplateParameter Members

        public string TemplateName { get; set; } = "Controller.tt";
        public string OutputPath { get; set; }
        public ComponentType Type { get; set; }
        public string FileName { get; set; }

        public ITemplateParameter Initialize(BusinessModelPackage package, CodeConfig config, object addtionalOptions = null)
        {
            this.InitializeForInterface(package.Set, config);
            OutputPath = config.ControllerPath;
            FileName = EntitySetName + "Controller.cs";
            var story = package.CrudUserStory;
            EditDtoName = story?.EditViewModel ?? EntityName + "Dto";
            ItemDtoName = story?.ListItemViewModel ?? EntityName + "Dto";

            return this;
        }

        #endregion
    }
}