using System;
using VerGen.Enums;
using VerGen.Ext;
using VerGen.Schema.Models;

namespace VerGen.TemplateParameters
{
    [Serializable]
    public class ServiceParameter : IServiceParameter, IEntityParameter, ITemplateParameter
    {
        public ServiceParameter()
        {
        }
        public string EntityNamespace { get; set; }
        public string EntityName { get; set; }
        public string EntityKeyType { get; set; }
        public string ServiceNamespace { get; set; }
        public string ServiceName { get; set; }
        public string TemplateName { get; set; } = "Service.tt";
        public string OutputPath { get; set; }
        public ComponentType Type { get; set; }
        public string FileName { get; set; }
        public ITemplateParameter Initialize(BusinessModelPackage package, CodeConfig config, object addtionalOptions = null)
        {
            OutputPath = config.ServicePath;
            FileName = ServiceName + ".cs";
            return this.InitializeForInterface(package.Set, config);

        }
    }
}